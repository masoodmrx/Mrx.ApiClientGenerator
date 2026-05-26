using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mrx.ApiClientGenerator.Models;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.TypeScript;

namespace Mrx.ApiClientGenerator.Core
{
    public static class ApiClientGeneratorService
    {
        public static async Task<bool> StartAsync(ProfileModel model, Func<ProfileModel, Task> generateDartClientAsync = null)
        {
            try
            {
                switch (model.Language)
                {
                    case Language.TypeScript:
                        await GenerateTypeScriptClient(model);
                        break;
                    case Language.CSharp:
                        await GenerateCSharpClient(model);
                        break;
                    case Language.Dart:
                        if (generateDartClientAsync == null)
                            throw new InvalidOperationException("Dart generator is not configured for this host.");
                        await generateDartClientAsync(model);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        private static async Task GenerateTypeScriptClient(ProfileModel model)
        {
            foreach (var generatePath in model.GeneratePath.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0))
            {
                GenerateClient(
                    document: await OpenApiDocument.FromUrlAsync(model.Url),
                    generatePath: generatePath,
                    generateCode: document =>
                    {
                        var settings = new TypeScriptClientGeneratorSettings();
                        settings.TypeScriptGeneratorSettings.TypeStyle = TypeScriptTypeStyle.Interface;
                        settings.TypeScriptGeneratorSettings.TypeScriptVersion = 3.5M;
                        settings.TypeScriptGeneratorSettings.DateTimeType = model.TypeScriptDateTimeType;
                        settings.TypeScriptGeneratorSettings.ExtensionCode = model.ExtensionCode;
                        settings.TypeScriptGeneratorSettings.TemplateDirectory = Path.Combine(AppContext.BaseDirectory, "Templates");
                        settings.Template = TypeScriptTemplate.Axios;
                        settings.ClientBaseClass = model.ClientBaseClass;
                        settings.UseGetBaseUrlMethod = model.UseGetBaseUrlMethod;
                        settings.UseTransformOptionsMethod = model.UseTransformOptionsMethod;
                        settings.UseTransformResultMethod = model.UseTransformResultMethod;
                        settings.UseAbortSignal = true;
                        settings.WrapDtoExceptions = true;

                        document.Host = model.BaseUrl;

                        var generator = new TypeScriptClientGenerator(document, settings);
                        return generator.GenerateFile();
                    });
            }
        }

        private static async Task GenerateCSharpClient(ProfileModel model)
        {
            GenerateClient(
                document: await OpenApiDocument.FromUrlAsync(model.Url),
                generatePath: model.GeneratePath,
                generateCode: document =>
                {
                    var settings = new CSharpClientGeneratorSettings
                    {
                        UseBaseUrl = false,
                        ClientBaseClass = model.ClientBaseClass,
                        UseHttpClientCreationMethod = true,
                        InjectHttpClient = false
                    };
                    settings.CSharpGeneratorSettings.Namespace = model.Name;
                    document.Host = model.BaseUrl;

                    var generator = new CSharpClientGenerator(document, settings);
                    return generator.GenerateFile();
                });
        }

        private static void GenerateClient(OpenApiDocument document, string generatePath, Func<OpenApiDocument, string> generateCode)
        {
            Console.WriteLine($"Generating {generatePath}...");
            var code = generateCode(document);
            File.WriteAllText(generatePath, code);
        }
    }
}

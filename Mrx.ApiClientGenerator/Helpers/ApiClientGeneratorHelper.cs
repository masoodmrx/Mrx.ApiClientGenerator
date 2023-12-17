using Mrx.ApiClientGenerator.Models;
using NJsonSchema.CodeGeneration;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.TypeScript;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mrx.ApiClientGenerator.Helpers
{
    public static class ApiClientGeneratorHelper
    {
        //dotnet run
        //--project C:/Users/MasoudMahdian/source/repos\WebApplication4/APIClientGenerator
        // https://localhost:6001/swagger/v1/swagger.json
        // C:/Users/MasoudMahdian/Desktop/my-app-react-ts/src/Apis/IraniViewApi.ts
        // TypeScript
        // baseUrl
        public static async Task<bool> Start(ProfileModel model)
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
                        await SwaggerCodegen.GenerateDartClient(model);
                        break;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private async static Task GenerateTypeScriptClient(ProfileModel model)
        {
            GenerateClient(
               document: await OpenApiDocument.FromUrlAsync(model.Url),
               generatePath: model.GeneratePath,
               generateCode: (OpenApiDocument document) =>
               {
                   var settings = new TypeScriptClientGeneratorSettings();

                   //var a = settings.TypeScriptGeneratorSettings.TemplateDirectory;
                   settings.TypeScriptGeneratorSettings.TypeStyle = TypeScriptTypeStyle.Interface;
                   settings.TypeScriptGeneratorSettings.TypeScriptVersion = 3.5M;
                   settings.TypeScriptGeneratorSettings.DateTimeType = model.TypeScriptDateTimeType;
                   settings.TypeScriptGeneratorSettings.ExtensionCode = model.ExtensionCode;
                   //settings.TypeScriptGeneratorSettings.TemplateFactory = model.ExtensionCode;
                   settings.TypeScriptGeneratorSettings.TemplateDirectory = Path.Combine(Application.StartupPath, "Templates");
                   //settings.TypeScriptGeneratorSettings.ExtensionCode = @"C:\Users\MasoudMahdian\Desktop\my-app-react-ts\src\Apis\BaseClass.tsx";
                   //settings.TypeScriptGeneratorSettings.Namespace = "Apis/BaseClass.ts";
                   //settings.TypeScriptGeneratorSettings.ExtendedClasses = new[] { "Apis/BaseClass.ts" };
                   // import { BaseClass } from './BaseClass';

                   //settings.TypeScriptGeneratorSettings.url = TypeScriptDateTimeType.String;
                   settings.Template = TypeScriptTemplate.Axios;
                   settings.ClientBaseClass = model.ClientBaseClass;
                   //settings.ConfigurationClass = "Apis/BaseClass.ts";
                   settings.UseGetBaseUrlMethod = model.UseGetBaseUrlMethod;
                   settings.UseTransformOptionsMethod = model.UseTransformOptionsMethod;
                   settings.UseTransformResultMethod = model.UseTransformResultMethod;

                   document.Host = model.BaseUrl;

                   var generator = new TypeScriptClientGenerator(document, settings);
                   var code = generator.GenerateFile();

                   return code;
               }
           );
        }

        private async static Task GenerateCSharpClient(ProfileModel model)
        {
            GenerateClient(
                document: await OpenApiDocument.FromUrlAsync(model.Url),
                generatePath: model.GeneratePath,
                generateCode: (OpenApiDocument document) =>
                {
                    var settings = new CSharpClientGeneratorSettings
                    {
                        UseBaseUrl = false,
                        ClientBaseClass = model.ClientBaseClass,
                        UseHttpClientCreationMethod = true,
                        InjectHttpClient = false,
                        //UseHttpRequestMessageCreationMethod = true,
                        //AdditionalNamespaceUsages = new[] { "Clinic24.Desktop.WebApi.Test.WebApiClient" },

                        //CodeGeneratorSettings=new CodeGeneratorSettingsBase
                        //{                            
                        //}
                    };
                    settings.CSharpGeneratorSettings.Namespace = model.Name;
                    document.Host = model.BaseUrl;

                    var generator = new CSharpClientGenerator(document, settings);
                    var code = generator.GenerateFile();
                    return code;
                }
            );
        }

        private static void GenerateClient(OpenApiDocument document, string generatePath, Func<OpenApiDocument, string> generateCode)
        {
            Console.WriteLine($"Generating {generatePath}...");

            var code = generateCode(document);

            File.WriteAllText(generatePath, code);
        }
    }

    //class MyTemplateFactory : NSwag.CodeGeneration.DefaultTemplateFactory
    //{
    //    //override 
    //    //public ITemplate CreateTemplate(string language, string template, object model)
    //    //{
    //    //    DefaultTemplateFactory
    //    //    return base.
    //    //}
    //    public MyTemplateFactory(CodeGeneratorSettingsBase settings, Assembly[] assemblies) : base(settings, assemblies)
    //    {
    //    }
    //    override 
    //}
}

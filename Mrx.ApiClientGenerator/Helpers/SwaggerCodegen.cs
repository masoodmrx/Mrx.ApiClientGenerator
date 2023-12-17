using Mrx.ApiClientGenerator.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Mrx.Common.Extensions;

namespace Mrx.ApiClientGenerator.Helpers
{
    public static class SwaggerCodegen
    {
        public static async Task GenerateDartClient(ProfileModel model)
        {
            await Run(model.Name, model.Url, model.GeneratePath, model.Language.ToString().ToLower());
            //await Run(model.Name, model.Url, model.GeneratePath, "dart-jaguar");

            var pubspecYamlPath = Path.Combine(model.GeneratePath, "pubspec.yaml");
            var pubspecYamlText = File.ReadAllText(pubspecYamlPath);
            pubspecYamlText = pubspecYamlText.Replace("\ndependencies:", "\nenvironment:\n  sdk: '>=2.10.0 <3.0.0'\ndependencies:");
            pubspecYamlText = pubspecYamlText.Replace("http: '>=0.11.1 <0.13.0'", "http: '>=0.13.0 <1.0.0'");
            File.WriteAllText(pubspecYamlPath, pubspecYamlText);

            var apiClientDartPath = Path.Combine(model.GeneratePath, "lib", "api_client.dart");
            var apiClientDartText = File.ReadAllText(apiClientDartPath);
            apiClientDartText = apiClientDartText.Replace("String url = basePath + path + queryString;", "Uri url = Uri.parse(basePath + path + queryString);");
            apiClientDartText = apiClientDartText.Replace("Uri.parse(url)", "url");
            File.WriteAllText(apiClientDartPath, apiClientDartText);

            var apiResultDartPath = Path.Combine(model.GeneratePath, "lib", "model", "api_result.dart");
            if (File.Exists(apiResultDartPath))
            {
                var apiResultDartText = File.ReadAllText(apiResultDartPath);
                apiResultDartText = apiResultDartText.Replace("new Object.fromJson(json['Result'])", "json['Result']");
                File.WriteAllText(apiResultDartPath, apiResultDartText);
            }

            var baseApiResultOfObjectPath = Path.Combine(model.GeneratePath, "lib", "model", "base_api_result_of_object.dart");
            if (File.Exists(baseApiResultOfObjectPath))
            {
                var baseApiResultOfObjectText = File.ReadAllText(baseApiResultOfObjectPath);
                baseApiResultOfObjectText = baseApiResultOfObjectText.Replace("new Object.fromJson(json['Result'])", "json['Result']");
                File.WriteAllText(baseApiResultOfObjectPath, baseApiResultOfObjectText);
            }

            //http: '>=0.11.1 <0.13.0'
            //http: '>=0.13.0 <1.0.0'
        }
        public static async Task Run(string name, string input, string output, string language)
        {
            File.WriteAllText("./swagger-codegen/config.json", new { pubName = name }.ToJson());
            if (input.StartsWith("http"))
            {
                var client = new HttpClient();
                var r = await client.GetStringAsync(input);
                input = "./swagger-codegen/input.json";
                File.WriteAllText(input, r);
            }

            var swaggerCodegenCliPath = "./swagger-codegen/swagger-codegen-cli.jar";
            var configFilePath = "./swagger-codegen/config.json";
            await RunCommand("java.exe", $@" -jar ""{swaggerCodegenCliPath}"" generate -i ""{input}"" -l {language} -o ""{output}"" -c ""{configFilePath}""");
        }
        private static async Task RunCommand(string command, string input)
        {
            await Task.Run(() =>
            {
                var processInfo = new ProcessStartInfo(command, input)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };
                Process proc;

                if ((proc = Process.Start(processInfo)) == null)
                {
                    throw new InvalidOperationException(" ?? ");
                }
                proc.BeginOutputReadLine();
                proc.OutputDataReceived += (s, e) =>
                {
                    Console.WriteLine(e.Data);
                };
                proc.WaitForExit();
                //int exitCode = proc.ExitCode;
                proc.Close();
                //Console.WriteLine(exitCode);                  
            });
        }
    }
}

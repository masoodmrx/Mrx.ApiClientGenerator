using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using Mrx.ApiClientGenerator.Helpers;
using Mrx.ApiClientGenerator.Models;
using NJsonSchema.CodeGeneration.TypeScript;

namespace Mrx.ApiClientGenerator
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                var exitCode = RunCliAsync(args).GetAwaiter().GetResult();
                Environment.Exit(exitCode);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static async System.Threading.Tasks.Task<int> RunCliAsync(string[] args)
        {
            try
            {
                var options = ParseArgs(args);
                if (options.ContainsKey("help") || options.ContainsKey("h") || options.ContainsKey("?"))
                {
                    PrintHelp();
                    return 0;
                }

                var model = BuildProfile(options);
                ValidateProfile(model);

                Console.WriteLine($"Generating profile '{model.Name}'...");
                var success = await ApiClientGeneratorHelper.Start(model);
                if (!success)
                {
                    Console.Error.WriteLine("Generation failed.");
                    return 2;
                }

                Console.WriteLine("Generation completed.");
                return 0;
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"Input error: {ex.Message}");
                PrintHelp();
                return 1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unhandled error: {ex.Message}");
                return 2;
            }
        }

        private static Dictionary<string, string> ParseArgs(string[] args)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (!arg.StartsWith("-"))
                    continue;

                var key = arg.TrimStart('-');
                var nextIsValue = i + 1 < args.Length && !args[i + 1].StartsWith("-");
                result[key] = nextIsValue ? args[++i] : "true";
            }
            return result;
        }

        private static ProfileModel BuildProfile(Dictionary<string, string> options)
        {
            var hasDirectArgs = options.ContainsKey("url") || options.ContainsKey("generate-path") || options.ContainsKey("language");
            return hasDirectArgs ? BuildProfileFromDirectArgs(options) : BuildProfileFromProfileFile(options);
        }

        private static ProfileModel BuildProfileFromProfileFile(Dictionary<string, string> options)
        {
            var profileFile = GetOption(options, "profile-file", Path.Combine(AppContext.BaseDirectory, "profiles.json"));
            if (!File.Exists(profileFile))
                throw new ArgumentException($"Profile file not found: {profileFile}");

            var data = JsonSerializer.Deserialize<FileModel>(File.ReadAllText(profileFile));
            if (data?.Profiles == null || data.Profiles.Count == 0)
                throw new ArgumentException($"No profiles found in: {profileFile}");

            if (options.TryGetValue("profile-index", out var indexText))
            {
                if (!int.TryParse(indexText, out var profileIndex))
                    throw new ArgumentException("profile-index must be a number.");
                if (profileIndex < 0 || profileIndex >= data.Profiles.Count)
                    throw new ArgumentException($"profile-index is out of range. Count: {data.Profiles.Count}");
                return data.Profiles[profileIndex];
            }

            var profileName = GetOption(options, "profile", null);
            if (string.IsNullOrWhiteSpace(profileName))
                return data.Profiles.First();

            var profile = data.Profiles.FirstOrDefault(x => string.Equals(x.Name, profileName, StringComparison.OrdinalIgnoreCase));
            if (profile == null)
                throw new ArgumentException($"Profile not found: {profileName}");

            return profile;
        }

        private static ProfileModel BuildProfileFromDirectArgs(Dictionary<string, string> options)
        {
            var languageText = GetRequiredOption(options, "language");
            if (!Enum.TryParse(languageText, true, out Language language))
                throw new ArgumentException("language is invalid. Use: TypeScript, CSharp, Dart");

            return new ProfileModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = GetOption(options, "name", "cli-profile"),
                ApiName = GetOption(options, "api-name", "api_client"),
                Url = GetRequiredOption(options, "url"),
                GeneratePath = GetRequiredOption(options, "generate-path"),
                Language = language,
                BaseUrl = GetOption(options, "base-url", null),
                ClientBaseClassStatus = GetFlag(options, "client-base-class-status"),
                ClientBaseClass = GetOption(options, "client-base-class", null),
                ExtensionCode = GetOption(options, "extension-code", null),
                UseGetBaseUrlMethod = GetFlag(options, "use-get-base-url-method"),
                UseTransformOptionsMethod = GetFlag(options, "use-transform-options-method"),
                UseTransformResultMethod = GetFlag(options, "use-transform-result-method"),
                TypeScriptDateTimeType = ParseTypeScriptDateTimeType(GetOption(options, "ts-datetime-type", "Date"))
            };
        }

        private static TypeScriptDateTimeType ParseTypeScriptDateTimeType(string value)
        {
            if (!Enum.TryParse(value, true, out TypeScriptDateTimeType dateType))
                throw new ArgumentException("ts-datetime-type is invalid.");
            return dateType;
        }

        private static void ValidateProfile(ProfileModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Url))
                throw new ArgumentException("url is required.");
            if (string.IsNullOrWhiteSpace(model.GeneratePath))
                throw new ArgumentException("generate-path is required.");
        }

        private static bool GetFlag(Dictionary<string, string> options, string key)
        {
            if (!options.TryGetValue(key, out var value))
                return false;
            if (string.IsNullOrWhiteSpace(value))
                return true;
            return value.Equals("true", StringComparison.OrdinalIgnoreCase) || value == "1";
        }

        private static string GetRequiredOption(Dictionary<string, string> options, string key)
        {
            var value = GetOption(options, key, null);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{key} is required.");
            return value;
        }

        private static string GetOption(Dictionary<string, string> options, string key, string defaultValue)
        {
            return options.TryGetValue(key, out var value) ? value : defaultValue;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Mrx.ApiClientGenerator CLI");
            Console.WriteLine("Usage:");
            Console.WriteLine("  Mrx.ApiClientGenerator.exe --profile-file <path> [--profile <name> | --profile-index <index>]");
            Console.WriteLine("  Mrx.ApiClientGenerator.exe --url <swaggerUrl> --generate-path <path[,path2]> --language <TypeScript|CSharp|Dart> [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --base-url <url>");
            Console.WriteLine("  --name <profileName>");
            Console.WriteLine("  --api-name <apiName>");
            Console.WriteLine("  --ts-datetime-type <Date|MomentJS|String|OffsetMomentJS|Luxon>");
            Console.WriteLine("  --client-base-class-status [true|false]");
            Console.WriteLine("  --client-base-class <className>");
            Console.WriteLine("  --extension-code <code>");
            Console.WriteLine("  --use-get-base-url-method [true|false]");
            Console.WriteLine("  --use-transform-options-method [true|false]");
            Console.WriteLine("  --use-transform-result-method [true|false]");
            Console.WriteLine("  --help");
        }
    }
}

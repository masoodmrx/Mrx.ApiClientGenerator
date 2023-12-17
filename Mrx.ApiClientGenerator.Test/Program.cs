//using java.io;
//using java.lang;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mrx.ApiClientGenerator.Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ////java -jar C:\Users\MasoudMahdian\Desktop\test dart swagger\swagger-codegen-cli.jar generate -i C:\Users\MasoudMahdian\Desktop\test dart swagger\IraniViewApi.json -l dart -o C:\Users\MasoudMahdian\Desktop\test dart swagger\dart_api_client -c C:\Users\MasoudMahdian\Desktop\test dart swagger\config.json
            ////var processInfo = new ProcessStartInfo("java.exe", @"-jar C:\Users\MasoudMahdian\Desktop\test dart swagger\swagger-codegen-cli.jar generate -i C:\Users\MasoudMahdian\Desktop\test dart swagger\IraniViewApi.json -l dart -o C:\Users\MasoudMahdian\Desktop\test dart swagger\dart_api_client -c C:\Users\MasoudMahdian\Desktop\test dart swagger\config.json")
            ////{
            ////    CreateNoWindow = true,
            ////    UseShellExecute = false,
            ////    RedirectStandardOutput = true
            ////};
            //var processInfo = new ProcessStartInfo("npm", " -v")
            //{
            //    CreateNoWindow = true,
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true
            //};
            //Process proc;

            //if ((proc = Process.Start(processInfo)) == null)
            //{
            //    throw new InvalidOperationException(" ?? ");
            //}

            //proc.OutputDataReceived += Proc_OutputDataReceived; ;
            //proc.WaitForExit();
            //var sr = proc.StandardOutput;
            //string output = sr.ReadToEnd();
            //int exitCode = proc.ExitCode;
            //proc.Close();
            //Console.WriteLine(output);
            //Console.WriteLine(exitCode);
            //Console.ReadKey();

            //com.github.fge.uritemplate.parse.URITemplateParser.parse()
            //new javax.validation.bootstrap.GenericBootstrap()
            //__WorkaroundBaseClass__.java.util.
            //new Codegen().
            //io.swagger.config
            //SwaggerCodegen
            //io.swagger.annotations.
            //DefaultGeneratorStrategy.
            //ClientOptInput
            //ClientOpts
            //ClientOptInput clientOptInput = new ClientOptInput();
            //ClientOpts clientOpts = new ClientOpts();

            //Swagger swagger = null;
            //swagger = new SwaggerParser().read(cmd.getOptionValue("i"), clientOptInput.getAuthorizationValues(), true);

            //io.swagger.pa

            //CommandLine cmd = null;

            //Run("npm -v", "");
            //Run(@"java -jar 'C:\Users\MasoudMahdian\Desktop\test dart swagger\swagger-codegen-cli.jar' generate -i 'C:\Users\MasoudMahdian\Desktop\test dart swagger\IraniViewApi.json' -l dart -o 'C:\Users\MasoudMahdian\Desktop\test dart swagger\dart_api_client' -c 'C:\Users\MasoudMahdian\Desktop\test dart swagger\config.json'", "");

            var inputPath = "https://localhost:6001/swagger/v1/swagger.json";
            var outputPath = @"C:\Users\MasoudMahdian\Desktop\test dart swagger\dart_api_client";
            await SwaggerCodegen(inputPath, outputPath, "dart");
            Console.ReadKey();
        }

        public static async Task<bool> SwaggerCodegen(string input, string output, string language)
        {
            if (input.StartsWith("http"))
            {
                var client = new HttpClient();
                var r = await client.GetStringAsync(input);
                File.WriteAllText("./input.json", r);
                input = "./input.json";
            }

            var swaggerCodegenCliPath = "./swagger-codegen-cli.jar";
            var configFilePath = "./config.json";
            return RunCommand("java.exe", $@" -jar ""{swaggerCodegenCliPath}"" generate -i ""{input}"" -l {language} -o ""{output}"" -c ""{configFilePath}""");
        }
        public static bool RunCommand(string command, string input)
        {
            try
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
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        //public static void Run(string command, string input)
        //{
        //    // create process
        //    string[] argss = { "cmd", "/c", command };
        //    ProcessBuilder pb = new ProcessBuilder(argss);
        //    Process process = pb.start();
        //    // create write reader
        //    BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(process.getOutputStream()));
        //    BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));
        //    // write input
        //    writer.write(input + "\n");
        //    writer.flush();
        //    // read output
        //    string line = "";
        //    while ((line = reader.readLine()) != null)
        //    {
        //        System.Console.WriteLine(line);
        //    }
        //    // wait for process to finish
        //    int returnValue = process.waitFor();
        //    // close writer reader
        //    reader.close();
        //    writer.close();
        //    System.Console.WriteLine("Exit with value " + returnValue);
        //}
    }
}

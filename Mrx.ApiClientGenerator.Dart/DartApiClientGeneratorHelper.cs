namespace Mrx.ApiClientGenerator.Dart
{
    public static class DartApiClientGeneratorHelper
    {
        public static async Task StartAsync(string inputFile, string outputDirectory, string? name = null)
        {
            await new DartApiClientGenerator(inputFile, outputDirectory, name).StartAsync();
        }
    }
}
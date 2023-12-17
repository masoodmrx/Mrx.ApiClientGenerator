using NJsonSchema.CodeGeneration.TypeScript;

namespace Mrx.ApiClientGenerator.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string GeneratePath { get; set; }
        public Language Language { get; set; }
        public TypeScriptDateTimeType TypeScriptDateTimeType { get; set; }
        public string BaseUrl { get; set; }
        public bool ClientBaseClassStatus { get; set; }
        public string ClientBaseClass { get; set; }
        public string ExtensionCode { get; set; }
        public bool UseGetBaseUrlMethod { get; set; }
        public bool UseTransformOptionsMethod { get; set; }
        public bool UseTransformResultMethod { get; set; }
    }
}

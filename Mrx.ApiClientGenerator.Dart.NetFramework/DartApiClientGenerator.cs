using CaseExtensions;
using Mrx.ApiClientGenerator.Dart.NetFramework.Helpers;
using Mrx.ApiClientGenerator.Dart.NetFramework.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Mrx.ApiClientGenerator.Dart.NetFramework
{
    public class DartApiClientGenerator
    {
        private readonly string inputFile;
        private readonly string outputDirectory;
        private readonly string name;
        public KeyValuePair<string, JsonNode>[] Definitions = new KeyValuePair<string, JsonNode>[] { };
        public KeyValuePair<string, JsonNode>[] ModelsDefinitions = new KeyValuePair<string, JsonNode>[] { };
        public KeyValuePair<string, JsonNode>[] EnumsDefinitions = new KeyValuePair<string, JsonNode>[] { };
        public List<string> Enums = new List<string>();


        public DartApiClientGenerator(string inputFile, string outputDirectory, string name = null)
        {
            this.inputFile = inputFile;
            this.outputDirectory = outputDirectory;
            this.name = name ?? Path.GetFileNameWithoutExtension(inputFile);
        }
        public async Task StartAsync()
        {
            string outputMainFile = Path.Combine(outputDirectory, $"{name}.swagger.dart");
            string outputEnumsFile = Path.Combine(outputDirectory, $"{name}.swagger.enums.dart");
            string outputMain = $@"// ignore_for_file: non_constant_identifier_names, constant_identifier_names, curly_braces_in_flow_control_structures

import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:irani_view/Swagger/{name}.swagger.enums.dart' as enums;

extension DynamicExtension on dynamic {{
  double? toDouble() {{
    return (this as int?)?.toDouble();
  }}
}}
";
            string outputEnums = @"// ignore_for_file: non_constant_identifier_names, constant_identifier_names
";
            string json;
            if (inputFile.IsValidUrl())
                json = await new HttpClient().GetStringAsync(inputFile);
            else
                json = File.ReadAllText(inputFile);


            //var obj = JsonSerializer.Deserialize<dynamic>(json);
            JsonNode data = JsonNode.Parse(json);

            Definitions = data["definitions"].AsObject().ToArray();
            ModelsDefinitions = Definitions.Where(p => p.Value["enum"] == null).ToArray();
            EnumsDefinitions = Definitions.Where(p => p.Value["enum"] != null).ToArray();
            Enums = EnumsDefinitions.Select(p => p.Key).ToList();



            var paths = data["paths"].AsObject().ToArray()
    .Select(p => new PathModel
    {
        Name = p.Key,
        Methods = p.Value.AsObject().ToArray()
        .Select(m =>
        {
            var response = m.Value?["responses"]?["200"];
            return new PathMethodModel
            {
                Name = m.Key,
                Tag = m.Value?["tags"]?.AsArray().Select(p2 => p2.GetValue<string>()).FirstOrDefault(),
                OperationId = (string)m.Value?["operationId"],
                Parameters = m.Value["parameters"]?.AsArray().ToArray().Select(pr =>
                {
                    var xSchemaRef = ((string)pr?["x-schema"]?["$ref"])?.Replace("#/definitions/", "");
                    return new PathMethodParameterModel
                    {
                        Name = (string)pr["name"],
                        In = (string)pr["in"],
                        Type = (string)pr["type"],
                        IsEnum = Enums.Contains(xSchemaRef),
                        Format = (string)pr["format"],
                        Required = (bool?)pr["required"],
                        IsNullable = (bool)pr["x-nullable"],
                        SchemaRef = ((string)pr?["schema"]?["$ref"])?.Replace("#/definitions/", ""),
                        XSchemaRef = xSchemaRef,
                    };
                })
                .ToList(),
                Response = response == null ? null : new PathMethodResponseModel
                {
                    IsNullable = (bool)response["x-nullable"],
                    Description = (string)response["description"],
                    SchemaRef = ((string)response["schema"]?["$ref"])?.Replace("#/definitions/", ""),
                }
            };
        })
        .ToList()
    })
    .ToList();


            foreach (var definition in EnumsDefinitions)
            {
                global::System.Console.WriteLine(definition.Key);
                var enumNames = definition.Value["x-enumNames"]?.AsArray().Select(p => p.GetValue<string>()).ToArray();
                var enumValues = definition.Value["enum"]?.AsArray().Select(p => p.GetValue<int>()).ToArray();
                outputEnums += $@"enum {definition.Key} {{ swaggerGeneratedUnknown, {enumNames.StringJoin(", ")} }}
const ${definition.Key}Map = {{{enumValues.Select((i, v) => $"{definition.Key}.{enumNames[i]}: {v}").StringJoin(", ")}}};

int? {definition.Key}ToJson({definition.Key}? {definition.Key}) {{
  return ${definition.Key}Map[{definition.Key}];
}}
{definition.Key} {definition.Key}FromJson(Object? value) {{
  if (value is int) {{
    var list = ${definition.Key}Map.entries.where((element) => element.value == value);
    if (list.isNotEmpty) return list.first.key;
  }}
  return {definition.Key}.swaggerGeneratedUnknown;
}}

";
            }
            foreach (var _definition in ModelsDefinitions)
            {
                KeyValuePair<string, JsonNode> definition = _definition;
                global::System.Console.WriteLine(definition.Key);
                var allOf = definition.Value["allOf"];
                if (allOf != null)
                {
                    var _ref = (string)allOf?[0]?["$ref"];
                    if (_ref != null)
                    {
                        _ref = _ref.Replace("#/definitions/", "");
                        definition = ModelsDefinitions.FirstOrDefault(p => p.Key == _ref);
                    }
                }
                string className = allOf == null ? definition.Key : _definition.Key;
                var required = definition.Value["required"]?.AsArray().Select(p => p.GetValue<string>()).ToArray();
                var properties = definition.Value["properties"]?.AsObject()?.ToArray()
                    .Select(p =>
                    {
                        var typeRef = (string)p.Value["$ref"];
                        var typeRefType = typeRef?.Replace("#/definitions/", "");

                        var itemsTypeRef = (string)p.Value["items"]?["$ref"];
                        var itemsTypeRefType = itemsTypeRef?.Replace("#/definitions/", "");
                        return new PropertyModel
                        {
                            Name = p.Key,
                            Required = required?.Contains(p.Key) ?? false,
                            Type = new PropertyTypeModel
                            {
                                Type = (string)p.Value["type"],
                                Format = (string)p.Value["format"],
                                Ref = typeRef,
                                RefType = typeRefType,
                                IsEnum = Enums.Contains(typeRefType),
                            },
                            ItemsType = p.Value["items"] == null ? null : new PropertyTypeModel
                            {
                                Type = (string)p.Value["items"]?["type"],
                                Format = (string)p.Value["items"]?["format"],
                                Ref = itemsTypeRef,
                                RefType = itemsTypeRefType,
                                IsEnum = Enums.Contains(itemsTypeRefType),
                            },
                        };
                    })
                    ?.ToList();
                outputMain += $@"
class {className} {{
{(properties.IsNull() ? "" : $"  {className}({{{properties?.Select(p => $"{(p.Required ? "required " : "")}this.{p.Name},").StringJoin("")}}});")}
{properties?.Select(p => $"  final {p.GetType()}{(p.IsNullable ? "?" : "")} {p.Name};").StringJoin("\r\n")}

  factory {className}.fromJson(Map<String, dynamic> json) => {className}({properties?.Select(p =>
                {
                    if (p.Type.IsEnum)
                        return $"{p.Name}: enums.{p.Type.RefType}FromJson(json['{p.Name}']),";
                    if (p.IsArray)
                        return $@"{p.Name}: (json['{p.Name}'] as List<dynamic>?)
                  ?.map((e) => {(
                            p.ItemsType.IsRef
                            ? $"{p.ItemsType?.RefType}.fromJson(e as Map<String, dynamic>)"
                            : $"e as {p.ItemsType?.GetType()}"
                            )})
                  .toList() ??
              [],";
                    if (p.Type.IsModel)
                        return $@"{p.Name}: json['{p.Name}'] == null
              ? null
              : {p.Type?.RefType}.fromJson(
                  json['{p.Name}'] as Map<String, dynamic>),";
                    //return $"'{p.Name}': {p.Name}{(p.IsNullable ? "?" : "")}.toJson(),";
                    if (p.Type.IsDateTime)
                        return $"{p.Name}: DateTime.parse(json['{p.Name}'] as String),";
                    if (p.Type.IsDouble)
                        return $"{p.Name}: json['{p.Name}']?.toDouble(),";
                    return $"{p.Name}: json['{p.Name}'] as {p.GetType()}{(p.IsNullable ? "?" : "")},";
                }).StringJoin("")});
  Map<String, dynamic> toJson() => <String, dynamic>{{{properties?.Select(p =>
                {
                    if (p.Type.IsEnum)
                        return $"'{p.Name}': enums.{p.Type.RefType}ToJson({p.Name}),";
                    if (p.IsArray && p.ItemsType.IsRef)
                        return $"'{p.Name}': {p.Name}{(p.IsNullable ? "?" : "")}.map((e) => e.toJson()).toList(),";
                    if (p.Type.IsModel)
                        return $"'{p.Name}': {p.Name}{(p.IsNullable ? "?" : "")}.toJson(),";
                    if (p.Type.IsDateTime)
                        return $"'{p.Name}': {p.Name}{(p.IsNullable ? "?" : "")}.toIso8601String(),";
                    return $"'{p.Name}': {p.Name},";
                }).StringJoin("")}}};
}}
{(properties.IsNull()
            ? ""
            : $@"
extension ${className}Extension on {className} {{
  {className} copyWith({{{properties.Select(p => $"{p.GetType()}{(p.IsDynamic ? "" : "?")} {p.Name},").StringJoin("")}}}) {{
    return {className}({properties.Select(p => $"{p.Name}: {p.Name} ?? this.{p.Name},").StringJoin("")});
  }}
}}
")}";
            }

            string outputApiPaths = "";
            foreach (var path in paths)
            {
                foreach (var method in path.Methods)
                {
                    var hasParams = method.Parameters.IsNotNull();
                    var hasParamsInBody = hasParams && method.Parameters.Any(p => p.In == "body");
                    var hasParamsInQuery = hasParams && method.Parameters.Any(p => p.In == "query");
                    var hasParamsInFormData = hasParams && method.Parameters.Any(p => p.In == "formData");
                    string headers = "{}";
                    switch (method.Name)
                    {
                        case "get":
                            headers = "{'Accept': 'text/plain'}";
                            break;
                        case "post":
                            if (hasParamsInFormData)
                                headers = "{'Content-Type': 'multipart/form-data', 'Accept': 'text/plain'}";
                            else
                                headers = "{'Content-Type': 'application/json', 'Accept': 'text/plain'}";
                            break;
                    }
                    outputApiPaths += $@"
  Future<{method.Response.SchemaRef}?> {method.OperationId.Replace("_", "")}{method.Name.ToPascalCase()}({(hasParams ? $"{{{method.Parameters.Select(p => $"{(p.IsNullable ? "" : "required ")}{p.GetType()}? {p.GetName()}, ").StringJoin("")}}}" : "")}) async {{
    var headers = {headers};
    var request = http.Request('{method.Name.ToUpper()}', Uri.parse('$baseUrl{path.Name}{(hasParamsInQuery ? $"?{method.Parameters.Where(p => p.In == "query").Select(p => $"{p.Name}=${p.GetName()}").StringJoin("&")}" : "")}'));
    {(hasParamsInBody ? $"request.body = json.encode({method.Parameters.First(p => p.In == "body").GetName()});" : "")}
    request.headers.addAll(headers);
    if (this.headers != null) request.headers.addAll(this.headers!);
    for (var requestInterceptor in requestInterceptors) requestInterceptor(request);

    http.StreamedResponse response = await request.send();
    for (var responseInterceptor in responseInterceptors) responseInterceptor(response);

    if (response.statusCode == 200) {{
      var res = await response.stream.bytesToString();
      return {method.Response.SchemaRef}.fromJson(json.decode(res));
    }} else {{
      if (log!) print(response.reasonPhrase);
    }}
    return null;
  }}
";
                }
            }

            outputMain += $@"

class {name} {{
  {name}({{
    this.baseUrl,
    this.log,
    this.headers,
    this.requestInterceptors = const [],
    this.responseInterceptors = const [],
  }});
  final String? baseUrl;
  final bool? log;
  final Map<String, String>? headers;
  final List<Function(http.Request)> requestInterceptors;
  final List<Function(http.StreamedResponse)> responseInterceptors;

  static {name} create({{
    String? baseUrl,
    bool? log,
    Map<String, String>? headers,
    List<Function(http.Request)>? requestInterceptors,
    List<Function(http.StreamedResponse)>? responseInterceptors,
  }}) {{
    return {name}(
      baseUrl: baseUrl ?? 'http://localhost:6000',
      log: log ?? true,
      headers: headers,
      requestInterceptors: requestInterceptors ?? [],
      responseInterceptors: responseInterceptors ?? [],
    );
  }}

  {outputApiPaths}
}}
";

            File.WriteAllText(outputMainFile, outputMain);
            File.WriteAllText(outputEnumsFile, outputEnums);
        }
    }
}
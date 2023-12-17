using CaseExtensions;
using Mrx.Common.Extensions;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;

string inPath = @"D:\Programing\Projects\IraniView\Source\IraniView.App\irani_view\lib\swaggers\IraniViewApi.json";
//string outPath = @"D:\Programing\Projects\App Utilities\Mrx.ApiClientGenerator Dart\dart project\api_test\lib\";
string outPath = @"D:\Programing\Projects\IraniView\Source\IraniView.App\irani_view\lib\Swagger\";
string outApiPath = Path.Combine(outPath, "IraniViewApi.swagger.dart");
string outApiEnumsPath = Path.Combine(outPath, "IraniViewApi.swagger.enums.dart");
string outputApi = @"// ignore_for_file: non_constant_identifier_names, constant_identifier_names, curly_braces_in_flow_control_structures

import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:irani_view/Swagger/IraniViewApi.swagger.enums.dart' as enums;

extension DynamicExtension on dynamic {
  double? toDouble() {
    return (this as int?)?.toDouble();
  }
}
";
string outputApiEnums = @"// ignore_for_file: non_constant_identifier_names, constant_identifier_names
";
string json = File.ReadAllText(inPath);
var obj = JsonSerializer.Deserialize<dynamic>(json);
JsonNode data = JsonNode.Parse(json);
var aa = data["definitions.{className}OfAccountGetProfileOutputApiModel"];
//.GetType().GetProperties().Select(p => p.Name).ToList()
//var a = data["definitions"].;


Static.Definitions = data["definitions"].AsObject().ToArray();
Static.ModelsDefinitions = Static.Definitions.Where(p => p.Value["enum"] == null).ToArray();
Static.EnumsDefinitions = Static.Definitions.Where(p => p.Value["enum"] != null).ToArray();
Static.Enums = Static.EnumsDefinitions.Select(p => p.Key).ToArray();

//var paths = data["paths"].AsObject().ToArray();
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
                Tag = m.Value?["tags"]?.AsArray().Select(p => p.GetValue<string>()).FirstOrDefault(),
                OperationId = (string)m.Value["operationId"],
                Parameters = m.Value["parameters"]?.AsArray().ToArray().Select(pr => new PathMethodParameterModel
                {
                    Name = (string)pr["name"],
                    In = (string)pr["in"],
                    Type = (string?)pr["type"],
                    Format = (string?)pr["format"],
                    Required = (bool?)pr["required"],
                    IsNullable = (bool)pr["x-nullable"],
                    SchemaRef = ((string?)pr?["schema"]?["$ref"])?.Replace("#/definitions/", ""),
                    XSchemaRef = ((string?)pr?["x-schema"]?["$ref"])?.Replace("#/definitions/", ""),
                })
                .ToList(),
                Response = response == null ? null : new PathMethodResponseModel
                {
                    IsNullable = (bool)response["x-nullable"],
                    Description = (string?)response["description"],
                    SchemaRef = ((string?)response["schema"]?["$ref"])?.Replace("#/definitions/", ""),
                }
            };
        })
        .ToList()
    })
    .ToList();


foreach (var definition in Static.EnumsDefinitions)
{
    global::System.Console.WriteLine(definition.Key);
    var enumNames = definition.Value["x-enumNames"]?.AsArray().Select(p => p.GetValue<string>()).ToArray();
    var enumValues = definition.Value["enum"]?.AsArray().Select(p => p.GetValue<int>()).ToArray();
    outputApiEnums += $@"enum {definition.Key} {{ swaggerGeneratedUnknown, {enumNames.StringJoin(", ")} }}
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
foreach (var _definition in Static.ModelsDefinitions)
{
    KeyValuePair<string, JsonNode?> definition = _definition;
    global::System.Console.WriteLine(definition.Key);
    var allOf = definition.Value["allOf"];
    if (allOf != null)
    {
        var _ref = (string?)allOf?[0]?["$ref"];
        if (_ref != null)
        {
            _ref = _ref.Replace("#/definitions/", "");
            definition = Static.ModelsDefinitions.FirstOrDefault(p => p.Key == _ref);
        }
    }
    string className = allOf == null ? definition.Key : _definition.Key;
    var required = definition.Value["required"]?.AsArray().Select(p => p.GetValue<string>()).ToArray();
    var properties = definition.Value["properties"]?.AsObject()?.ToArray()
        .Select(p => new PropertyModel
        {
            Name = p.Key,
            Required = required?.Contains(p.Key) ?? false,
            Type = new PropertyTypeModel
            {
                Type = (string)p.Value["type"],
                Format = (string?)p.Value["format"],
                Ref = (string?)p.Value["$ref"],
            },
            ItemsType = p.Value["items"] == null ? null : new PropertyTypeModel
            {
                Type = (string?)p.Value["items"]?["type"],
                Format = (string?)p.Value["items"]?["format"],
                Ref = (string?)p.Value["items"]?["$ref"],
            },
        })
        ?.ToList();
    outputApi += @$"
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
        outputApiPaths += @$"
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

outputApi += @$"

class IraniViewApi {{
  IraniViewApi({{
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

  static IraniViewApi create({{
    String? baseUrl,
    bool? log,
    Map<String, String>? headers,
    List<Function(http.Request)>? requestInterceptors,
    List<Function(http.StreamedResponse)>? responseInterceptors,
  }}) {{
    return IraniViewApi(
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

//JsonArray jsonArray = (JsonArray)obj2["definitions"];
File.WriteAllText(outApiPath, outputApi);
File.WriteAllText(outApiEnumsPath, outputApiEnums);
//var definitions = obj.definitions as dynamic[];
//foreach (var definition in definitions)
//{
//    var name = definition;
//}
//var obj2 = JsonSerializer.Deserialize<dynamic>(json);

public static class Static
{
    public static KeyValuePair<string, JsonNode?>[] Definitions = new KeyValuePair<string, JsonNode?>[] { };
    public static KeyValuePair<string, JsonNode?>[] ModelsDefinitions = new KeyValuePair<string, JsonNode?>[] { };
    public static KeyValuePair<string, JsonNode?>[] EnumsDefinitions = new KeyValuePair<string, JsonNode?>[] { };
    public static string[] Enums = new string[] { };

    //public static List<PathModel> Paths { get; set; }
    //public static List<PathModel> Paths { get; set; }
}
class PathModel
{
    public string Name { get; set; }
    public List<PathMethodModel> Methods { get; set; }
}
class PathMethodModel
{
    public string Name { get; set; }
    public string? Tag { get; set; }
    public string OperationId { get; set; }
    public List<PathMethodParameterModel> Parameters { get; set; }
    public PathMethodResponseModel Response { get; set; }
}
class PathMethodParameterModel
{
    /// <summary>
    /// name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// in
    /// </summary>
    public string In { get; set; }//formData , query , body
    /// <summary>
    /// required
    /// </summary>
    public bool? Required { get; set; }
    /// <summary>
    /// schema/$ref
    /// </summary>
    public string? SchemaRef { get; set; }
    /// <summary>
    /// x-schema/$ref
    /// </summary>
    public string? XSchemaRef { get; set; }
    /// <summary>
    /// x-nullable
    /// </summary>
    public bool IsNullable { get; set; }
    public string? Type { get; set; }//string , boolean , array , number , file
    public string? Format { get; set; }//date-time , double , int32

    public bool IsRef => SchemaRef.IsNotNullOrEmpty() || XSchemaRef.IsNotNullOrEmpty();
    public bool IsEnum => Static.Enums.Contains(XSchemaRef);

    public string GetName()
    {
        return Name.Replace(".", "");
    }
    public string GetType()
    {
        switch (Type)
        {
            case "boolean":
                return "bool";
            case "string":
                switch (Format)
                {
                    case "date-time":
                        return "DateTime";
                }
                return "String";
            case "number":
                return "double";
            case "integer":
                return "int";
            case "file":
                return "http.MultipartFile";
        }
        if (IsRef)
            return IsEnum ? "enums." + XSchemaRef : SchemaRef;
        return "dynamic";
    }
}
class PathMethodResponseModel
{
    /// <summary>
    /// description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// schema/$ref
    /// </summary>
    public string? SchemaRef { get; set; }
    /// <summary>
    /// x-nullable
    /// </summary>
    public bool IsNullable { get; set; }
}
class PropertyModel
{
    public string Name { get; set; }
    public bool Required { get; set; }
    public bool IsDynamic => Type.Type.IsNullOrEmpty() && Type.Ref.IsNullOrEmpty();
    public bool IsNullable => !IsDynamic && !Required;
    public bool IsArray => Type.IsArray;
    public PropertyTypeModel Type { get; set; }
    public PropertyTypeModel? ItemsType { get; set; }

    public string GetType()
    {
        if (Type.IsArray)
            return $"List<{ItemsType.GetType()}>";
        return Type.GetType();
    }
}
class PropertyTypeModel
{
    public string Type { get; set; }//string , boolean , array , number
    public string? Format { get; set; }//date-time , double
    public string? Ref { get; set; }
    public bool IsRef => Ref?.Contains("#/definitions/") ?? false;
    public string? RefType => Ref?.Replace("#/definitions/", "");
    public bool IsArray => Type == "array";
    public bool IsModel => IsRef && !IsEnum;
    public bool IsEnum => Static.Enums.Contains(RefType);
    public bool IsDateTime => Type == "string" && Format == "date-time";
    public bool IsDouble => Type == "number" && (Format == "double" || Format == "float");
    public string GetType()
    {
        switch (Type)
        {
            case "boolean":
                return "bool";
            case "string":
                switch (Format)
                {
                    case "date-time":
                        return "DateTime";
                }
                return "String";
            case "number":
                return "double";
            case "integer":
                return "int";
        }
        if (IsRef)
            return IsEnum ? "enums." + RefType : RefType;
        return "dynamic";
    }
}

//Form Data , inner model , array model

//var headers = {
//  'Content-Type': 'multipart/form-data',
//  'Accept': 'text/plain'
//};
//var request = http.MultipartRequest('POST', Uri.parse('https://localhost:6001/Admin/AdminCategory/Create'));
//request.fields.addAll({
//    'Name': '<string>',
//  'Status': '1'
//});
//request.files.add(await http.MultipartFile.fromPath('Image', '/path/to/file'));
//request.headers.addAll(headers);

//http.StreamedResponse response = await request.send();

//if (response.statusCode == 200)
//{
//    print(await response.stream.bytesToString());
//}
//else
//{
//    print(response.reasonPhrase);
//}

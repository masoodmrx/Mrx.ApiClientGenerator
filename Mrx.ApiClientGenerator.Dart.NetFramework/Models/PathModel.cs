using Mrx.ApiClientGenerator.Dart.NetFramework.Helpers;
using System.Collections.Generic;

namespace Mrx.ApiClientGenerator.Dart.NetFramework.Models
{
    public class PathModel
    {
        public string Name { get; set; }
        public List<PathMethodModel> Methods { get; set; }
    }
    public class PathMethodModel
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string OperationId { get; set; }
        public List<PathMethodParameterModel> Parameters { get; set; }
        public PathMethodResponseModel Response { get; set; }
    }
    public class PathMethodParameterModel
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
        public string SchemaRef { get; set; }
        /// <summary>
        /// x-schema/$ref
        /// </summary>
        public string XSchemaRef { get; set; }
        /// <summary>
        /// x-nullable
        /// </summary>
        public bool IsNullable { get; set; }
        public string Type { get; set; }//string , boolean , array , number , file
        public string Format { get; set; }//date-time , double , int32

        public bool IsRef => SchemaRef.IsNotNullOrEmpty() || XSchemaRef.IsNotNullOrEmpty();
        public bool IsEnum { get; set; }

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
    public class PathMethodResponseModel
    {
        /// <summary>
        /// description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// schema/$ref
        /// </summary>
        public string SchemaRef { get; set; }
        /// <summary>
        /// x-nullable
        /// </summary>
        public bool IsNullable { get; set; }
    }
    public class PropertyModel
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public bool IsDynamic => Type.Type.IsNullOrEmpty() && Type.Ref.IsNullOrEmpty();
        public bool IsNullable => !IsDynamic && !Required;
        public bool IsArray => Type.IsArray;
        public PropertyTypeModel Type { get; set; }
        public PropertyTypeModel ItemsType { get; set; }

        public string GetType()
        {
            if (Type.IsArray)
                return $"List<{ItemsType.GetType()}>";
            return Type.GetType();
        }
    }
    public class PropertyTypeModel
    {
        public string Type { get; set; }//string , boolean , array , number
        public string Format { get; set; }//date-time , double
        public string Ref { get; set; }
        public bool IsRef => Ref?.Contains("#/definitions/") ?? false;
        public string RefType { get; set; }
        public bool IsArray => Type == "array";
        public bool IsModel => IsRef && !IsEnum;
        public bool IsEnum { get; set; }
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
}
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FocLab.Configuration.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        readonly bool EnumAsInt = false;

        public void Apply(Schema schema, SchemaFilterContext context)
        {
            UpdateEnumDefinitions(schema, context.SystemType);
        }

        /// <summary>
        /// https://github.com/Azure/swashbuckle-resource-provider/blob/master/SwashApiTest/Swagger/SwaggerSchemaFilter.cs
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="type"></param>
        private void UpdateEnumDefinitions(Schema schema, Type type)
        {
            if (schema.Properties == null)
                return;

            foreach (var property in schema.Properties)
            {
                //Фикс для массива из перечислений 
                if (property.Value.Items != null && property.Value.Items.@Enum != null)
                {
                    var modelType = type.GetProperty(property.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).PropertyType;

                    var isEnumerable = typeof(IEnumerable).IsAssignableFrom(modelType) && modelType != typeof(string);

                    if (!isEnumerable)
                    {
                        return;
                    }

                    var clrType = modelType.IsArray ? modelType.GetElementType() : modelType.GetGenericArguments().FirstOrDefault();

                    property.Value.Items.Extensions.Add("x-ms-enum", new
                    {
                        name = clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>)
                            ? clrType.GetGenericArguments()[0].Name
                            : clrType.Name,
                        modelAsString = false,
                        values = BuildEnumNameValueList(clrType)
                    });
                }



                if (property.Value.@Enum != null)
                {
                    var clrType = type.GetProperty(property.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).PropertyType;

                    property.Value.Extensions.Add("x-ms-enum", new
                    {
                        name = clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>)
                            ? clrType.GetGenericArguments()[0].Name
                            : clrType.Name,
                        modelAsString = false,
                        values = BuildEnumNameValueList(clrType)
                    });
                }
            }
        }

        private IEnumerable<dynamic> BuildEnumNameValueList(Type type)
        {
            if (type.IsGenericType)
            {
                //it's a nullable enum
                type = Nullable.GetUnderlyingType(type) ?? type;
            }

            if (EnumAsInt)
            {
                return from object e in Enum.GetValues(type)
                       select new
                       {
                           name = e.ToString(),
                           value = (int)e
                       };
            }

            return from object e in Enum.GetValues(type)
                   select new
                   {
                       name = e.ToString(),
                       value = e.ToString()
                   };
        }


    }
}

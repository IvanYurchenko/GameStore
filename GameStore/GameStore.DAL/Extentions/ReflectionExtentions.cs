using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using System.Linq;
using System.Reflection;

namespace GameStore.DAL.Extentions
{
    public static class ReflectionExtentions
    {
        public static PropertyInfo[] VisibleProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName()).ToArray();
        }

        public static string IdentifierPropertyName(this Object model)
        {
            Type type = ObjectContext.GetObjectType(model.GetType());
            return IdentifierPropertyName(type);
        }

        public static string IdentifierPropertyName(this Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            
            string result = string.Empty;

            if (properties.Any(x => Attribute.IsDefined(x, typeof (KeyAttribute), false)))
            {
                result =
                    properties.Select(x => x)
                        .First(x => Attribute.IsDefined(x, typeof (KeyAttribute), false))
                        .Name;
            }
            else
            {
                string expectedPropName = "Id";

                if (
                    properties.Any(
                        x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    result = properties
                        .First(x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase))
                        .Name;
                }
                else
                {
                    expectedPropName = type.Name + "Id";

                    if (
                        properties.Any(
                            x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        result = properties
                            .First(
                                x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase))
                            .Name;
                    }
                }
            }

            return result;
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
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
            var type = ObjectContext.GetObjectType(model.GetType());
            return IdentifierPropertyName(type);
        }

        public static string IdentifierPropertyName(this Type type)
        {
            var properties = type.GetProperties();

            if (properties.Any(x => Attribute.IsDefined(x, typeof (KeyAttribute), false)))
            {
                return
                    properties.Select(x => x)
                        .First(x => Attribute.IsDefined(x, typeof (KeyAttribute), false))
                        .Name;
            }

            var expectedPropName = "Id";

            if (properties.Any(x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return properties
                    .First(x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase))
                    .Name;
            }

            expectedPropName = type.Name + "Id";

            if (properties.Any(x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return properties
                    .First(x => String.Equals(x.Name, expectedPropName, StringComparison.CurrentCultureIgnoreCase))
                    .Name;
            }

            return "";
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
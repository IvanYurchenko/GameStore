using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Extentions
{
    public static class ProxyExtentions
    {
        public static bool IsProxy(this object obj)
        {
            return obj != null && ObjectContext.GetObjectType(obj.GetType()) != obj.GetType();
        }
        
        public static T UnProxy<T>(this T proxyObject, DbContext context) where T : class
        {
            var proxyCreationEnabled = context.Configuration.ProxyCreationEnabled;
            try
            {
                context.Configuration.ProxyCreationEnabled = false;
                T poco = context.Entry(proxyObject).CurrentValues.ToObject() as T;
                return poco;
            }
            finally
            {
                context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            }
        }
    }
}

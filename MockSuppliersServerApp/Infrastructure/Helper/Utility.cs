using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Infrastructure
{
    public interface IUtility {
        public Dictionary<string, string> GetValidAttrErrorsByReflection(object classObj);
    }
    public class Utility: IUtility
    {
        public Dictionary<string, string> GetValidAttrErrorsByReflection(object classObj) 
        {
            var dict = new Dictionary<string, string>();

            PropertyInfo[] classProperties = classObj.GetType().GetProperties();
            foreach (var classProp in classProperties)
                if (classProp.GetCustomAttribute<RequiredAttribute>() != null && classProp.GetValue(classObj) == null)
                    dict.Add(classProp.Name, classProp.GetCustomAttribute<RequiredAttribute>().ErrorMessage);

            return dict;
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using MockSuppliersServerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace MockSuppliersServerApp.Infrastructure.ModelBinders
{
    public class ZConsignDetailsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var xmlStr = bindingContext.ActionContext.HttpContext.Request.QueryString.Value.Trim('?');
            if (!string.IsNullOrEmpty(xmlStr))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ZConsignDetailsModel));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(HttpUtility.UrlDecode(xmlStr)));
                                
                var result = ((ZConsignDetailsModel)serializer.Deserialize(ms));

                var _utility = bindingContext.HttpContext.RequestServices.GetService(typeof(IUtility)) as Utility;

                foreach (var error in _utility.GetValidAttrErrorsByReflection(result))
                    bindingContext.ModelState.AddModelError(error.Key, error.Value);

                if (bindingContext.ModelState.IsValid)
                    bindingContext.Result = ModelBindingResult.Success(result);
            }
            return Task.CompletedTask;
        }
    }
}

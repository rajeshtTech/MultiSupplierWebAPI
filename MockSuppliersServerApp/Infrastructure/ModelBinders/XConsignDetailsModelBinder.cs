using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using MockSuppliersServerApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace MockSuppliersServerApp.Infrastructure.ModelBinders
{
    public class XConsignDetailsModelBinder: IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var jsonString = bindingContext.ActionContext.HttpContext.Request.QueryString.Value.Trim('?');
            XConsignDetailsModel result = JsonConvert.DeserializeObject<XConsignDetailsModel>(HttpUtility.UrlDecode(jsonString));
            
            if (result != null)
            {
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

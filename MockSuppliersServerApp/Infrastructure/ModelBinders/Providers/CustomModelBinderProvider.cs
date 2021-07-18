using Microsoft.AspNetCore.Mvc.ModelBinding;
using MockSuppliersServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Infrastructure.ModelBinders.Providers
{
    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(XConsignDetailsModel))
                return new XConsignDetailsModelBinder();
            else if (context.Metadata.ModelType == typeof(YConsignDetailsModel))
                return new YConsignDetailsModelBinder();
            else if (context.Metadata.ModelType == typeof(ZConsignDetailsModel))
                return new ZConsignDetailsModelBinder();

            return null;
        }
    }
}

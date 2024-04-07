using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnanasMVCWebApp.Areas.Admin.Data {
    public class CustomProductBinding : IModelBinder {
        public Task BindModelAsync(ModelBindingContext bindingContext) {
            throw new NotImplementedException();
        }
    }
}

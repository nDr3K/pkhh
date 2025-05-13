using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace PokeSaveRomManager.Api.Shared.ModelBinders
{
    public class JsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            try
            {
                var json = valueProviderResult.FirstValue;
                var result = JsonSerializer.Deserialize(json, bindingContext.ModelType, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }

}

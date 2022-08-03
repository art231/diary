using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diary.Api.Configuration
{
    public sealed class FromJsonAttribute : ModelBinderAttribute
    {
        public FromJsonAttribute(string name = "data") : base(typeof(JsonFormDataModelBinder))
        {
            Name = name;
        }
    }

    public class JsonFormDataModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var value = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (value == ValueProviderResult.None)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                    bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(
                        bindingContext.FieldName));
            }
            else
            {
                using MemoryStream? memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value.FirstValue!));
                bindingContext.Result = ModelBindingResult.Success(
                    await JsonSerializer.DeserializeAsync(memoryStream, bindingContext.ModelType,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            }
        }
    }
}

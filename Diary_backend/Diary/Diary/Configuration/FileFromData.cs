using Diary.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Api.Configuration
{
    public sealed class FromFileFormDataAttribute : ModelBinderAttribute
    {
        public FromFileFormDataAttribute(string name) : base(typeof(FileFormDataModelBinder))
        {
            Name = name;
        }
    }

    public sealed class FileFormData : List<IFormFile>
    {
        private readonly FileStoreSettings _fileStoreSettings;

#pragma warning disable CS8618
        public FileFormData() { }
#pragma warning restore CS8618

        public FileFormData(List<IFormFile> files, FileStoreSettings fileStoreSettings)
        {
            _fileStoreSettings = fileStoreSettings;
            AddRange(files);
        }

        /// <summary>
        ///     Признак существования файлов
        /// </summary>
        public bool Empty => Count == 0;

        /// <summary>
        ///     Вернет результат
        /// </summary>
        /// <param name="fileStoreTypes"></param>
        /// <returns></returns>
        public (bool ok, string? value) IsValid(FileStoreTypes fileStoreTypes = FileStoreTypes.Default)
        {
            if (Empty) return (false, "Список файлов пуст");
            var setting = _fileStoreSettings.ContainsKey(fileStoreTypes) ? _fileStoreSettings[fileStoreTypes] : null;
            if (setting == null) return (false, $"Настройка - {fileStoreTypes} не найдена");

            var fileWithErrors = new StringBuilder(Count);

            if (Count > setting.MaxCount)
                fileWithErrors.AppendLine(
                    $"Превышено максимальное количество файлов. Файлов {Count} при максимально допустимом {setting.MaxCount}");

            ForEach(x =>
            {
                var size = GetSizeInMb(x.Length);
                if (size > setting.MaxSize)
                    fileWithErrors.AppendLine(
                        $"Превышен максимальный размер файла в {setting.MaxSize}MB для файла - {x.FileName}({size}MB)");
            });

            return fileWithErrors.Length == 0 ? (true, null) : (false, fileWithErrors.ToString());
        }

        /// <summary>
        ///     Прочитает файлы из формы
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<(string Name, byte[] Data)>> ReadFilesAsync()
        {
            var files = new List<(string Name, byte[] Data)>();
            await foreach (var file in ReadFileStoreAsync())
            {
                files.Add(file);
            }
            return files.AsReadOnly();
        }

        /// <summary>
        ///     Прочитает файлы из формы
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<(string Name, byte[] Data)> ReadFileStoreAsync()
        {
            foreach (var file in this)
            {
                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                yield return (file.FileName, memoryStream.ToArray());
            }
        }

        private static double GetSizeInMb(long fileLength)
        {
            var value = (double)fileLength / 1024 / 1024;
            var pow = Math.Pow(10.0, 2);
            return Math.Truncate(pow * value) / pow;
        }

    }

    public class FileFormDataModelBinder : IModelBinder
    {
        private readonly FileStoreSettings _fileStoreSettings;
        private readonly FormFileModelBinder _formFileModelBinder;

        public FileFormDataModelBinder(ILoggerFactory loggerFactory, FileStoreSettings fileStoreSettings)
        {
            _fileStoreSettings = fileStoreSettings;
            _formFileModelBinder = new FormFileModelBinder(loggerFactory);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(FileFormData)) return;

            var model = bindingContext.ModelMetadata;
            List<IFormFile>? files = null;
            var fieldName = model.BinderModelName!;
            using (bindingContext.EnterNestedScope(model, fieldName, fieldName, null))
            {
                await _formFileModelBinder.BindModelAsync(bindingContext);
                if (bindingContext.Result.IsModelSet)
                    files = bindingContext.Result.Model as List<IFormFile>;
            }

            bindingContext.ModelState.Remove(fieldName);
            bindingContext.Result = ModelBindingResult.Success(new FileFormData(files ?? new List<IFormFile>(), _fileStoreSettings));
        }
    }
}

using System.Collections.Generic;

namespace Diary.Infrastructure.Settings
{
    /// <summary>
    ///     Настройки файлового хранилища
    /// </summary>
    public class FileStoreSettings : Dictionary<FileStoreTypes, FileStoreSetting>
    {
    }

    /// <summary>
    ///     Настройка файлового хранилища
    /// </summary>
    public class FileStoreSetting
    {
        /// <summary>
        ///     Максимальный размер в МБ
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        ///     Максимальное количество файлов
        /// </summary>
        public int MaxCount { get; set; }
    }

    /// <summary>
    ///     Тип настроек
    /// </summary>
    public enum FileStoreTypes
    {
        Default
    }
}

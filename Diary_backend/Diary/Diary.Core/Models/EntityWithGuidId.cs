using System.ComponentModel.DataAnnotations;

namespace Diary.Core.Models;

public sealed class EntityWithGuidId
{

    [Key]
    public string Id { get; set; } = string.Empty;
}

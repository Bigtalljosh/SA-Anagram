using System.ComponentModel.DataAnnotations;

namespace Anagram;

public class ApplicationArguments
{
    /// <summary>
    /// The filename of the Dictionary File to use. Currently we only support .txt files.
    /// </summary>
    [Required]
    public string DictionaryFileName { get; set; }
}

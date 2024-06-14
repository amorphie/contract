using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace amorphie.contract.application.Migration;

public class DysDocumentInputDto
{

    private readonly string[] formats = { "yyyy-MM-dd HH:mm:ss.fffffff", "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-dd HH:mm:ss.ff", "yyyy-MM-dd HH:mm:ss.f", "yyyy-MM-dd HH:mm:ss.ffff", "yyyy-MM-dd HH:mm:ss.fffff", "yyyy-MM-dd HH:mm:ss.ffffff", "yyyy-MM-dd HH:mm:ss.fffffff" };

    public DysDocumentInputDto() { }

    [Required]
    public required int DocId { get; set; }
    public string OwnerId { get; set; }
    public int OtoDmsId { get; set; }
    public string Title { get; set; }

    public DateTime CreatedAt { get; set; }
    public string CreateTime
    {
        set
        {
            string[] formats = { "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-dd HH:mm:ss.ff", "yyyy-MM-dd HH:mm:ss.f", "yyyy-MM-dd HH:mm:ss.ffff", "yyyy-MM-dd HH:mm:ss.fffff", "yyyy-MM-dd HH:mm:ss.ffffff", "yyyy-MM-dd HH:mm:ss.fffffff" };
            if (DateTime.TryParseExact(value, formats, null, DateTimeStyles.None, out DateTime date))
            {
                CreatedAt = date;
            }
            else
            {
                throw new ArgumentException($"Unable to parse date with format {string.Join(", ", formats)} or any other accepted format.");
            }

        }
    }

    public string Notes { get; set; }
    public int IsDeleted { get; set; }

    [Required]
    public required int TagId { get; set; }
    public string TagValue { get; set; }
    public string CBOperation { get; set; }
    public string CTOperation { get; set; }


    public Dictionary<string, string> ParseTagValue()
    {
        var keyValuePairs = new Dictionary<string, string>();

        if (String.IsNullOrEmpty(TagValue))
            return keyValuePairs;

        // Identify the outer tag dynamically
        var outerTagStartIndex = TagValue.IndexOf('<') + 1;
        var outerTagEndIndex = TagValue.IndexOf('>');
        if (outerTagStartIndex == 0 || outerTagEndIndex == -1)
            throw new FormatException("Invalid format for TAGVALUE.");

        var outerTag = TagValue.Substring(outerTagStartIndex, outerTagEndIndex - outerTagStartIndex);
        var outerCloseTag = $"</{outerTag}>";

        // Remove the outer tags
        var innerContent = TagValue.Replace($"<{outerTag}>", "").Replace(outerCloseTag, "").Trim();

        // Split by opening and closing tags
        while (innerContent.Length > 0)
        {
            var startIndex = innerContent.IndexOf('<');
            var endIndex = innerContent.IndexOf('>');

            if (startIndex == -1 || endIndex == -1)
                break;

            // Extract the key
            var key = innerContent.Substring(startIndex + 1, endIndex - startIndex - 1);

            // Find the closing tag
            var closeTag = $"</{key}>";
            var closeTagIndex = innerContent.IndexOf(closeTag);

            if (closeTagIndex == -1)
                break;

            // Extract the value
            var value = innerContent.Substring(endIndex + 1, closeTagIndex - endIndex - 1);

            // Add to dictionary
            keyValuePairs[key] = value;

            // Remove the processed part
            innerContent = innerContent.Substring(closeTagIndex + closeTag.Length).Trim();
        }

        return keyValuePairs;
    }

}

using System.Globalization;

namespace amorphie.contract.application.Migration;

public class KafkaBase
{
    public string Magic { get; set; } = default!;
    public string Type { get; set; } = default!;
    public object? Headers { get; set; }
    public object? MessageSchemaId { get; set; }
    public object? MessageSchema { get; set; }
}

public class KafkaData<T> : KafkaBase
{
    public KafkaMessage<T> Message { get; set; } = default!;
}

public class KafkaMessage<T>
{
    public T Data { get; set; } = default!;
    public object? BeforeData { get; set; }
    public object? Headers { get; set; }
}

public class DysDocumentTagKafkaInputDto
{
    private static readonly HashSet<string> AllowedTagIds = new() { "1685" };

    public long DocId { get; set; }
    public string TagId { get; set; } = default!;
    public string TagValue { get; set; } = default!;

    public bool IsAllowedTagId()
    {
        return AllowedTagIds.Contains(TagId);
    }

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

public class DysDocumentKafkaInputDto
{
    /// <summary>
    /// indicates DocId
    /// </summary>
    public required long Id { get; set; }
    public int OtoDmsId { get; set; }
    public string Title { get; set; } = default!;
    public string Channel { get; set; }
    public string Notes { get; set; }
    public string OwnerId { get; set; }
    public byte IsDeleted { get; set; }

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

}


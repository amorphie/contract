using System.Globalization;

namespace amorphie.contract.application.Migration;

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


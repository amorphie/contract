using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application;

public class DocumentDownloadInputDto
{
    [Required]
    public required string ObjectId { get; set; }

    private string _userReference;

    public void SetUserReference(string userReference)
    {
        if (String.IsNullOrEmpty(userReference))
            throw new ArgumentNullException($"{nameof(userReference)} cannot be null");


        _userReference = userReference;
    }

    public string GetUserReference()
    {
        return _userReference;
    }

}

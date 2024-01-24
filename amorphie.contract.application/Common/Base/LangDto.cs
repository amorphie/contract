namespace amorphie.contract.application;


//TODO: tüm proje için genel bir tanımlama yapılacak.
public class LangDto
{
    public string? LangCode { get; set; }

    public string GetLangCode()
    {
        return LangCode is null ? "en-EN" : LangCode;
    }
}



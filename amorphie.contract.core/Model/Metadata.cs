namespace amorphie.contract.core.Model
{
    public class Metadata
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
        public bool IsRequired { get; set; }
        public string InputType { get; set; }

        public bool IsValidTagData()
        {
            return Data.StartsWith("$tag.");
        }
    }
}
using System.Text.RegularExpressions;

namespace amorphie.contract.core.Model
{
    public class Metadata
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
        public bool IsRequired { get; set; }
        public string InputType { get; set; }

        public bool IsTagData()
        {
            return Data.StartsWith("$tag.");
        }

        public bool? IsValidTag(){
            if (IsTagData()){
                string pattern = @"^\$tag\.[a-zA-Z0-9-]+\.[a-zA-Z0-9-]+\.[a-zA-Z0-9-]+\.(reference|customerNo)$";
                Regex regex = new Regex(pattern);
                bool isValid = regex.IsMatch(Data);
                return isValid;
            }
            else {
                return null;
            }
        }
    }
}
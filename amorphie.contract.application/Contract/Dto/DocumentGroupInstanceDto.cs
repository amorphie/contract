using System.Text.Json.Serialization;

namespace amorphie.contract.application
{
    public class DocumentGroupInstanceDto
    {
        public bool Required { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }

        public int AtLeastRequiredDocument { get; set; }
        public DocumentGroupDetailInstanceDto DocumentGroupDetailInstanceDto { get; set; }
    }
}

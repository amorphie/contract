using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.core.Model.Proxy
{
    public class SendTemplatedMailRequestModel
    {
        public string Sender { get; set; }
        public string Email { get; set; }
        public string Template { get; set; }
        public string TemplateParams { get; set; }
        public List<SendTemplatedMailAttachmentModel> Attachments { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public int CustomerNo { get; set; }
        public string CitizenshipNo { get; set; }
        public List<string> Tags { get; set; }
        public bool IsVerified { get; set; }
        public bool InstantReminder { get; set; }
        public SendTemplatedMailProcessModel Process { get; set; }
    }
}
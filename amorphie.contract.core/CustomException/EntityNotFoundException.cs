using System;
using System.Net;

namespace amorphie.contract.core.CustomException
{
	public sealed class EntityNotFoundException : CustomBaseException
	{
        public override string MessageFormat => "{entityName} Not Found ({identifier}).";
        public override string Title => "Not Found Error";
        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        public EntityNotFoundException(string entityName, string identifier) : base()
        {
            MessageProps.Add("{entityName}", entityName);
            MessageProps.Add("{identifier}", identifier);
        }
	}
}


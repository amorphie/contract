using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;

namespace amorphie.contract.zeebe.Services.Interfaces
{
    public interface IDocumentDefinitionService
    {
        Task<DocumentDefinition> DataModelToDocumentDefinition(DocumentDefinitionDataModel documentDefinitionDataModel);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;

namespace amorphie.contract.core.Services
{
    public interface IContractInstanceService
        {
            Task Start(Contract contract,string Language);
            Task UploadFile();
        }
    public class ContractInstanceService
    {
        
    }
}
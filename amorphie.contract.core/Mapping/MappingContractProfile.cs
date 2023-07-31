using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Contract;
using AutoMapper;

namespace amorphie.contract.core.Mapping
{
    public class MappingContractProfile:Profile
    {
        public MappingContractProfile()
        {
             CreateMap<ContractDefinition, ContractDefinition>().ReverseMap();
             CreateMap<ContractDocumentDetail, ContractDocumentDetail>().ReverseMap();
             CreateMap<ContractEntityProperty, ContractEntityProperty>().ReverseMap();
             
        }
    }
   
}
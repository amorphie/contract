using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Document;
using AutoMapper;

namespace amorphie.contract
{
    public sealed class ResourceMapper : Profile
    {
        public ResourceMapper()
        {
            CreateMap<DocumentTag, DocumentTag>().ReverseMap();
        }
    }
}
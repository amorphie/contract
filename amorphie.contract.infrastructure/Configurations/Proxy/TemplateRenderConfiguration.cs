﻿using System;
using amorphie.contract.core.Entity.Proxy;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amorphie.contract.infrastructure.Configurations.Proxy
{
    public class TemplateRenderConfiguration
    {
        public TemplateRenderConfiguration(EntityTypeBuilder<TemplateRender> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}


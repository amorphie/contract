﻿using System.Text.Json.Serialization;

namespace amorphie.contract.application.Contract.Dto
{

    public class DocumentInstanceDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string? UseExisting { get; set; }
        public string? MinVersion { get; set; }
        public bool IsRequired { get; set; }
        public DocumentInstanceDetailDto DocumentDetail { get; set; } = new DocumentInstanceDetailDto();

    }
}

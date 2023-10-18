using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.zeebe.Services.Interfaces
{
  public interface IMinioService
  {
    Task UploadFile(byte[] data, string objectName, string contentType);
    Task UploadFile();
  }
}
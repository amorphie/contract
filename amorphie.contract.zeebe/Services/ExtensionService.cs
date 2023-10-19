using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.zeebe.Services.Interfaces;

namespace amorphie.contract.zeebe.Services
{
    public static class ExtensionService
    {
        public static byte[] StringToBytes(string filebyte, string count)
        {
            List<byte> bytes = new List<byte>();
            var arrayfile = filebyte.Split(',');
            foreach (var i in arrayfile)
            {
                bytes.Add(Convert.ToByte(i));

            }
            return bytes.ToArray();
        }
    }
}
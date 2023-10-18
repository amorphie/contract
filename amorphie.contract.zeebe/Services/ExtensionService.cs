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
            byte[] bytes = new byte[Convert.ToInt32(count)];
            var arrayfile = filebyte.Split(',');
            foreach (var i in arrayfile)
            {
                bytes.Append<byte>(Convert.ToByte(i));

            }
            return bytes;
        }
    }
}
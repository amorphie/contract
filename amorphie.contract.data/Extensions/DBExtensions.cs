using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.data.Contexts;

namespace amorphie.contract.data.Extensions
{
    public static class DBExtensions
    {

        public static ProjectDbContext? projectDbContext;
        public static LanguageType LanguageTypeCreateOrGet(string value)
        {
            var firs = projectDbContext?.LanguageType.FirstOrDefault(x => x.Name == value);
            if (firs == null)
            {
                firs = new LanguageType
                {
                    Name = value
                };
                projectDbContext?.LanguageType.Add(firs);
                projectDbContext?.SaveChanges();
            }
            return firs;
        }

    }
}
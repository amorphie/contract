using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.data.Contexts
{
    public class DbInitializer
    {
        public static void Initialize(ProjectDbContext context)
        {
            context.Database.EnsureCreated();
            if(context.DocumentTag!=null)
            context.DocumentTag.Add(new core.Entity.Document.DocumentTag{Code="firstCode", Contact="FirstContract"});
            context.SaveChanges();
            var a = context.DocumentTag.First();
        }
    }
}
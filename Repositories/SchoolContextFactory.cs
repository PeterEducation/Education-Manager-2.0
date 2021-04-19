using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace Repositories
{
    public class SchoolContextFactory : IDbContextFactory<SchoolContext>
    {
        public SchoolContext Create()
        {
            return new SchoolContext("Data Source=(local);Initial Catalog=School2;User Id=sa;Password=bergsee_neu7#_pwd_8@3;MultipleActiveResultSets=True;");
        }
    }
}

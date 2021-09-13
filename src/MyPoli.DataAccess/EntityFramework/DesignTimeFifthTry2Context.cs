using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyPoli.DataAccess.EntityFramework
{
    public class DesignTimeFifthTry2Context: IDesignTimeDbContextFactory<FifthTry2Context>
    {
        public FifthTry2Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FifthTry2Context>();
            // pass your design time connection string here
            optionsBuilder.UseSqlServer("Data Source=ASTANCA;Initial Catalog=FifthTry2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new FifthTry2Context(optionsBuilder.Options);
        }
    }
}

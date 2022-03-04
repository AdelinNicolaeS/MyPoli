using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyPoli.DataAccess.EntityFramework
{
    public class DesignTimeFifthTry2Context: IDesignTimeDbContextFactory<MyPoliContext>
    {
        public MyPoliContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyPoliContext>();
            // pass your design time connection string here
          //  optionsBuilder.UseSqlServer("Data Source=ASTANCA;Initial Catalog=FifthTry2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-T2SS4A8;Initial Catalog=MyPoli;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new MyPoliContext(optionsBuilder.Options);
        }
    }
}

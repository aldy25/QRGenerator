using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QRGenerator.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.DataAccess
{
    public static class Depedencies
    {
        public static void AddDataAccessServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QrgeneratorContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("QRGeneratorConnection"))
                );
        }
    }
}

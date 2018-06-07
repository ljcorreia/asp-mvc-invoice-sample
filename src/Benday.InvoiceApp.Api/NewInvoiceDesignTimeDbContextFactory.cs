using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Benday.InvoiceApp.Api
{
    public class NewInvoiceDesignTimeDbContextFactory : IDesignTimeDbContextFactory<InvoiceDbContext>
    {
        public NewInvoiceDesignTimeDbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public NewInvoiceDesignTimeDbContextFactory(IConfiguration configuration, string connectionStringConfigurationName = "default") : this(configuration)
        {
            ConnectionStringConfigurationName = connectionStringConfigurationName;
        }

        private IConfiguration Configuration { get; set; }


        public string ConnectionStringConfigurationName { get; set; }

        public InvoiceDbContext Create()
        {
            var connectionString = GetConnectionString();

            return Create(connectionString);
        }

        public InvoiceDbContext CreateDbContext(string[] args)
        {
            var connectionString = GetConnectionString();

            return Create(connectionString);
        }

        private string GetConnectionString()
        {
            var connectionString = Configuration.GetConnectionString(ConnectionStringConfigurationName);

            //check valid configuration connection string
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find a connection string named 'default'.");
            }

            return connectionString;
        }

        private InvoiceDbContext Create(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<InvoiceDbContext>();
            builder.UseSqlServer(connectionString);
            return new InvoiceDbContext(builder.Options);
        }
    }
}

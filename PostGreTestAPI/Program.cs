
using Microsoft.EntityFrameworkCore;
using PostGreTestAPI.DataAccess;

namespace PostGreTestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add and test out the database connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (!ConnectionStringValidator.ValidateConnectionString(connectionString))
            {
                throw new Exception("Invalid connection string.");
            }

            // Register ApplicationDbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Add healthchecks
            builder.Services.AddHealthChecks().AddNpgSql(connectionString, name: "PostgreSQL");

            // Add default API services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            // Map health check endpoint
            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}

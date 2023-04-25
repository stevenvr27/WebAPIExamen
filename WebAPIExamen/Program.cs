using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAPIExamen.Models;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
            
        var cnnStrBuilder = new SqlConnectionStringBuilder(
            builder.Configuration.GetConnectionString("CNNSTR"));

        string cnnStr = cnnStrBuilder.ConnectionString;

        builder.Services.AddDbContext<AnswersDBContext>(options => options.UseSqlServer(cnnStr));


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI( );
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
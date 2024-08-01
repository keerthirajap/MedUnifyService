namespace MedUnify.AuthAPI
{
    using FluentValidation.AspNetCore;
    using MedUnify.AuthAPI.DbContext;
    using MedUnify.AuthAPI.Repositories;
    using MedUnify.AuthAPI.Repositories.Concrete;
    using MedUnify.AuthAPI.Services.Concrete;
    using MedUnify.AuthAPI.Services.Interface;
    using MedUnify.ResourceModel;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Access configuration
            var configuration = builder.Configuration;

            string absoluteProjectFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\..\Database\"));

            string sqlConnectionString = builder.Configuration.GetConnectionString("MedUnifyDb");

            sqlConnectionString = sqlConnectionString.Replace("__ReplaceWithProjectFolder__", absoluteProjectFolderPath);

            // Configure services
            builder.Services.AddDbContext<MedUnifyDbContext>(options =>
                options.UseSqlServer(sqlConnectionString));

            builder.Services.AddScoped<IOAuthClientRepository, OAuthClientRepository>();
            builder.Services.AddScoped<IOAuthClientService, OAuthClientService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register FluentValidation
            builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ResourceModels>());

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

            app.Run();
        }
    }
}
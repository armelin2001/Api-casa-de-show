using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Api_casa_de_show.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using Api_casa_de_show.Repositorio;
namespace Api_casa_de_show
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //https://localhost:5001/swagger/index.html
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddDbContext<ApplicationDbContext>(options=>options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(config=>{
                config.SwaggerDoc("v1", new OpenApiInfo{Title = "Api",Version="v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
            services.AddTransient<CasaDeShowRepositorio>();
            services.AddTransient<EventoRepositorio>();
            services.AddTransient<GeneroRepositorio>();
            services.AddTransient<UsuarioRepositorio>();
            services.AddTransient<VendaRepositorio>();
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);
            IdentityModelEventSource.ShowPII = true;
            services.AddAuthentication(options =>{
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>{
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(config=>{
                config.SwaggerEndpoint("/swagger/v1/swagger.json","v1 docs");
            });
            
        }
    }
}

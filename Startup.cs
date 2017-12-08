using ContactsWebApi.Config;
using ContactsWebApi.Models;
using ContactsWebApi.Repositories;
using ContactsWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ContactsWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // ADD service interface + service //
            /* kutsun aikana: samaa instanssia käytetään */
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository,ContactRepository>();
            //services.AddSingleton<IContactRepository, ContactRepository>();

            //cors ongelman ohittamiseksi sallitaan yhteydet kaikkialta
            services.AddCors(o => o.AddPolicy("ContactsAppPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));


            services.AddMvc();
            //register in memory database called ContactList
            //services.AddDbContext<ContactContext>(opt => opt.UseInMemoryDatabase("ContactList"));
            //Configuration["ConnectionString"] 
            //ConnectionStrings
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=ContactsWebApi.ContactsDB;Trusted_Connection=True;";
            services.AddDbContext<ContactContext>(options =>
                options.UseSqlServer(
                    Configuration["ConnectionStringAzure"]
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("ContactsAppPolicy");
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ContactContext>();
                context.Database.EnsureCreated();
            }
            app.UseMvc();
        }
    }
}

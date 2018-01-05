using ContactsWebApi.Config;
using ContactsWebApi.Repositories;
using ContactsWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository,ContactRepository>();

            //cors ongelman ohittamiseksi sallitaan yhteydet kaikkialta
            services.AddCors(o => o.AddPolicy("ContactsAppPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddMvc();
            services.AddDbContext<ContactContext>(options =>
                options.UseSqlServer(
                    Configuration["ConnectionStringAzure"]
            ));

            services.AddAuthentication(options =>
            {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
              options.Audience = Configuration["AzureSettings:ApplicationId"];
              options.Authority = Configuration["AzureSettings:AuthorityUrl"]; 
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
             app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
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

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.BLL.Storages;
using OnlineBanking.Mapper;
using OnlineBanking.Extensions.Services;

namespace OnlineBanking
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath($"{environment.WebRootPath}")
                .AddJsonFile("appsettings.json")
                .AddJsonFile("tokensettings.json")
                .AddJsonFile("emailsettings.json")
                .Build();

            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDateBaseContext(Configuration, Environment);
            services.AddAutoMapper(mapper => mapper.AddProfile(new AutoMapperProfile()));
            services.AddServices();
            services.AddSingleton<IUserTwoFactorTokenStorage, UserTwoFactorStaticTokenStorage>();
            services.AddIdentity();
            services.AddBearerAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

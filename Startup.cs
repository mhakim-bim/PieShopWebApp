using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Auth;
using PieShop.Models;

namespace PieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("PieShop")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789- ._@+";
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = false;

                }
                 )
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = "409211356930730";
                    facebookOptions.AppSecret = "b7e2c286adf0f29917ff6c57bab5597d";
                }
            );

            services.AddTransient<IPieRepository, PieRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();

            //RequireSsl will be removed in next version..
            services.AddAntiforgery(opts => {opts.RequireSsl = true; });

            services.AddMvc(options => 
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes => routes.MapRoute(name: "Default",
                template: "{controller=Pie}/{action=Index}/{id?}"));
        }
    }
}

namespace MDManagement.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using MDManagement.Web.Data;
    using MDManagement.Data.Models;
    using MDManagement.Services;
    using MDManagement.Services.Implementations;
    using MDManagement.Services.Data;
    using MDManagement.Services.Data.Implementations;

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
            services.AddDbContext<MDManagementDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<Employee>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MDManagementDbContext>();

            //services.AddAuthorization( options => 
            //{
            //    options.AddPolicy("Test");
            //});

            services.AddTransient<UserManager<Employee>>();
            services.AddTransient<IDepatmentDataService, DepatmentDataService>();
            services.AddTransient<ITownDataService, TownDataService>();
            services.AddTransient<ICompanyDataService, CompanyDataSerive>();
            services.AddTransient<IJobTitleDataService, JobTittleDataServie>();
            services.AddTransient<IEmployeeDataService, EmployeeDataService>();

            services.AddTransient<IComapnyService, ComapnyService>();
            services.AddTransient<IJobTitleService, JobTitleService>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

using ConfigLibrary.Lib.Services;

namespace ServiceA
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IConfigurationService>(s => new ConfigurationService("ConfigLibraryApiService", "mongodb://localhost:27017"));
            //var provider = services.BuildServiceProvider();
            //var myService = provider.GetService<IConfigurationService>();
            //Configuration["SiteName"] = await myService.GetValue<string>("SiteName");
            //services.Configure<SettingsModel>(Configuration);
            //Console.WriteLine(Configuration["SiteName"]);

            services.AddSingleton(s => new ConfigurationService("SERVICEA", "mongodb://localhost:27017",20000));
        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
        }
    }
}

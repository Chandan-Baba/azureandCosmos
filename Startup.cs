using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts2TableCosmosMVC.Models;
using Contacts2TableCosmosMVC.Models.Abstract;
using Contacts2TableCosmosMVC.Models.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace Contacts2TableCosmosMVC
{
  public class Startup
  {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      #region Utilities
      services.Configure<StorageUtility>(cfg =>
      {
        cfg.StorageAccountName = _configuration["StorageAccountInformation:StorageAccountName"];
        cfg.StorageAccountAccessKey = _configuration["StorageAccountInformation:StorageAccountAccessKey"];
      });
      services.Configure<CosmosUtility>(cfg =>
      {
        cfg.CosmosEndpoint = _configuration["CosmosConnectionString:CosmosEndpoint"];
        cfg.CosmosKey = _configuration["CosmosConnectionString:CosmosKey"];
      });
      #endregion

      #region ApplicationInsights
      services.AddApplicationInsightsTelemetry(cfg =>
      {
        cfg.InstrumentationKey = _configuration["ApplicationInsights:InstrumentationKey"];
      });
      services.AddLogging(cfg =>
      {
        cfg.AddApplicationInsights(_configuration["ApplicationInsights:InstrumentationKey"]);
        // Optional: Apply filters to configure LogLevel Information or above is sent to
        // ApplicationInsights for all categories.
        cfg.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);

        // Additional filtering For category starting in "Microsoft",
        // only Warning or above will be sent to Application Insights.
        //cfg.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
      });
      #endregion

      #region Database
      if (string.IsNullOrEmpty(_configuration["DatabaseType"]) || _configuration["DatabaseType"] == "AzureTable")
      { services.AddScoped<IContactRepository, TableContactRepository>(); }
      if (_configuration["DatabaseType"] == "CosmosDb")
      { services.AddScoped<IContactRepository, CosmosContactRepository>(); }
      #endregion

      #region Swagger
      services.AddSwaggerGen(cfg =>
      {
        cfg.SwaggerDoc(name: "V1", info: new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Contacts API", Version = "V1" });
      });
      #endregion

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
        app.UseExceptionHandler("/Home/Error");
      }

      var appInsightsFlag = app.ApplicationServices.GetService<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>();
      if (_configuration["DisableAppInsightsTelemetry"] == "false")
      {
        appInsightsFlag.DisableTelemetry = false;
      }
      else
      {
        appInsightsFlag.DisableTelemetry = true;
      }


      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseSwagger();
      app.UseSwaggerUI(cfg =>
      {
        cfg.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "Contact API");
      });

      app.UseMvc(ConfigureRoutes);

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }

    private void ConfigureRoutes(IRouteBuilder routeBuilder)
    {
      routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
    }
  }
}

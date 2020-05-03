using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Contacts2TableCosmosMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;

namespace Contacts2TableCosmosMVC.Controllers
{
  public class HomeController : Controller
  {
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> Vault()
    {
      AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
      KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
      var secrets = await keyVaultClient.GetSecretsAsync($"{_configuration["CBKeyVault"]}");

      Dictionary<string, string> secretValueList = new Dictionary<string, string>();
      foreach (var item in secrets)
      {
        var secret = await keyVaultClient.GetSecretAsync(item.Id);
        secretValueList.Add(item.Id, secret.Value);
      }
      return View(secretValueList);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}

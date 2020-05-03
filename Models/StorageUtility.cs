using Microsoft.WindowsAzure.Storage;

namespace Contacts2TableCosmosMVC.Models
{
  public class StorageUtility
  {
    public string StorageAccountName { get; set; }
    public string StorageAccountAccessKey { get; set; }
    public CloudStorageAccount StorageAccount
    {
      get
      {
        string account = StorageAccountName;
        if (account == "{StorageAccountName}")
        {
          return CloudStorageAccount.DevelopmentStorageAccount;
        }
        else
        {
          string key = StorageAccountAccessKey;
          string connectionString = $"DefaultEndpointsProtocol=https;AccountName={account};AccountKey={key}";
          return CloudStorageAccount.Parse(connectionString);
        }
      }
    }
  }
}
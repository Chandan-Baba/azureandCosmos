
using Microsoft.WindowsAzure.Storage.Table;

namespace Contacts2TableCosmosMVC.Models.Entities
{
  public class ContactTable : TableEntity
  {
    public ContactTable() { }
    public ContactTable(string contactName, string phone)
    {
      PartitionKey = contactName;
      RowKey = phone;
    }
    public string Id { get; set; }
    public string ContactType { get; set; }
    public string Email { get; set; }
  }
}
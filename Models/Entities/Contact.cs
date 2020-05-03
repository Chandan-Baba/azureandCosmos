using Newtonsoft.Json;

namespace Contacts2TableCosmosMVC.Models.Entities
{
  public class Contact
  {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "contactName")]
    public string ContactName { get; set; }
    [JsonProperty(PropertyName = "phone")]
    public string Phone { get; set; }
    [JsonProperty(PropertyName = "contactType")]
    public string ContactType { get; set; }
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }
  }
}
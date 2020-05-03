using System.ComponentModel.DataAnnotations;

namespace Contacts2TableCosmosMVC.ViewModels
{
  public class ContactViewModel
  {
    public string Id { get; set; }
    [Required(ErrorMessage = "Contact Name cannot be empty")]
    public string ContactName { get; set; }
    [Required(ErrorMessage = "Phone cannot be empty")]
    public string Phone { get; set; }
    [Required(ErrorMessage = "Contact Type cannot be empty")]
    public string ContactType { get; set; }
    [Required(ErrorMessage = "Email cannot be empty"), EmailAddress(ErrorMessage = "Enter a Valid Email Address")]
    public string Email { get; set; }
  }
}

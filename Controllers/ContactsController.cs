using System.Collections.Generic;
using System.Threading.Tasks;
using Contacts2TableCosmosMVC.Models.Abstract;
using Contacts2TableCosmosMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contacts2TableCosmosMVC.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ContactsController : ControllerBase
  {
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactController> _logger;

    public ContactsController(IContactRepository contactRepository, ILogger<ContactController> logger)
    {
      _contactRepository = contactRepository;
      _logger = logger;
    }

    [Route("")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> Get()
    {
      var result = await _contactRepository.GetAllContactsAsync();
      return Ok(result);
    }

    [Route("/id/{id}")]
    [HttpGet]
    public async Task<ActionResult<Contact>> Get(string id)
    {
      var result = await _contactRepository.FindContactAsync(id);
      return Ok(result);
    }

    [Route("/name/{contactName}")]
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetByName(string contactName)
    {
      var result = await _contactRepository.FindContactsByContactNameAsync(contactName);
      return Ok(result);
    }

    [Route("/phone/{phone}")]
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetByPhone(string phone)
    {
      var result = await _contactRepository.FindContactByPhoneAsync(phone);
      return Ok(result);
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult<Contact>> Post(Contact contact)
    {
      var result = await _contactRepository.CreateAsync(contact);
      return Created("/", result);
    }

    [Route("/{id}/{contactName}/{phone}")]
    [HttpDelete]
    public async Task<ActionResult> Delete(string id, string contactName, string phone)
    {
      await _contactRepository.DeleteAsync(id, contactName, phone);
      return Ok();
    }
  }
}
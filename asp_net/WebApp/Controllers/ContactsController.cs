using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Entities;
using System.Threading.Tasks;
using WebApp.Contexts;

public class ContactsController : Controller
{
    private readonly ContactContext _contactContext;

    public ContactsController(ContactContext contactContext)
    {
        _contactContext = contactContext;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Contact Us";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitMessage(string firstName, string email, string mobile, string company, string message)
    {
        var contactMessage = new ContactMessageEntity
        {
            Name = firstName,
            PhoneNumber = mobile,
            Company = company,
            Email = email,
            Message = message,
            Date = DateTime.UtcNow
        };

        _contactContext.RecievedMessages.Add(contactMessage);
        await _contactContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}

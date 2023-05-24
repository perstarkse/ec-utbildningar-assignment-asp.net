using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;
using WebApp.Models.Entities;

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
        return View(new ContactFormViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> SubmitMessage(ContactFormViewModel model)
    {
        if (ModelState.IsValid)
        {
            var contactMessage = new ContactMessageEntity
            {
                Name = model.FirstName,
                PhoneNumber = model.Mobile,
                Company = model.Company,
                Email = model.Email,
                Message = model.Message,
                Date = DateTime.UtcNow
            };

            _contactContext.RecievedMessages.Add(contactMessage);
            await _contactContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Message successfully submitted, we'll contact you.";

            return RedirectToAction("Index");
        }

        // If the model is not valid, return the view with the validation errors
        return View("Index", model);
    }
}
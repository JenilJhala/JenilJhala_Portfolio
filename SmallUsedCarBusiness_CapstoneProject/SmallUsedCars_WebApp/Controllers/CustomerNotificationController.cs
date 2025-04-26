using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    public class CustomerNotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerNotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /CustomerNotification/NotificationForm?customerId=123&defaultMessage=...
        public IActionResult NotificationForm(int? customerId, string defaultMessage = null)
        {
            var model = new CustomerNotificationViewModel();

            if (customerId.HasValue)
            {
                var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId.Value);
                if (customer != null)
                {
                    model.Email = customer.Email;
                    model.CustomerName = $"{customer.FirstName} {customer.LastName}";
                }
                else
                {
                    model.CustomerName = "Unknown Customer";
                    model.Email = "";
                }
            }

            // Pre-populate default subject and message.
            model.Subject = "Maintenance Reminder";
            model.Message = !string.IsNullOrEmpty(defaultMessage)
                ? defaultMessage
                : "Dear Customer, your vehicle is due for regular maintenance. Please schedule your service appointment at your earliest convenience.";

            return View(model);
        }

        // POST: /CustomerNotification/NotificationForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NotificationForm(CustomerNotificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Optionally create a Notification record for logging purposes.
            var notification = new Notification
            {
                NotificationType = "Maintenance Reminder",
                Subject = model.Subject,
                Message = $"{model.Message}\nNotification sent to: {model.Email}",
                SentDate = DateTime.Now
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Here you could integrate your email service to actually send the email.
            TempData["SuccessMessage"] = "Notification sent successfully.";
            return RedirectToAction("Success");
        }

        // GET: /CustomerNotification/NotificationSuccess
        public IActionResult Success()
        {
            return View();
        }
    }
}

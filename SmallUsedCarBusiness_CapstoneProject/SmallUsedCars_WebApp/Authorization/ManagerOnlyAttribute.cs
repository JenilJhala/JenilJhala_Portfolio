using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmallUsedCars_WebApp.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmallUsedCars_WebApp.Authorization
{
    public class ManagerOnlyAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Retrieve UserManager<Employee> from DI
            var userManager = (UserManager<Employee>)context.HttpContext.RequestServices.GetService(typeof(UserManager<Employee>));
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            var employee = await userManager.FindByIdAsync(userId);
            if (employee == null || string.IsNullOrEmpty(employee.Position) ||
                !employee.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

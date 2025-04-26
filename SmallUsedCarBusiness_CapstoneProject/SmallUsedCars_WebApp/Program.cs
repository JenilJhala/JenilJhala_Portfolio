using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarDatabase")));

builder.Services.AddIdentity<Employee, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Register your services
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<SalesService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<PayrollService>();
builder.Services.AddScoped<HRService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmailService>();
// ... other service registrations

var app = builder.Build();

// Seed the Identity data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedIdentityDataAsync(services);
}

async Task SeedIdentityDataAsync(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<Employee>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create roles if they don't exist.
    string[] roleNames = { "Manager", "Employee" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Create the manager account if it does not exist.
    string managerEmail = "matt@example.com";
    var managerUser = await userManager.FindByEmailAsync(managerEmail);
    if (managerUser == null)
    {
        managerUser = new Employee
        {
            Id = "f47ac10b-58cc-4372-a567-0e02b2c3d479",
            EmployeeName = "Matt Smith",
            Position = "Manager",  // This is key for manager access if you use Position checks.
            Department = "Sales",
            ContactNumber = "541-222-3574",
            HireDate = DateTime.Now.AddYears(-10),
            CurrentPositionStartDate = DateTime.Now.AddYears(-2),
            UserName = "mattsmith",
            Email = managerEmail,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await userManager.CreateAsync(managerUser, "Admin@123");
        if (result.Succeeded)
        {
            // Optionally assign the Manager role (if you use both role and position checks)
            await userManager.AddToRoleAsync(managerUser, "Manager");
        }
        else
        {
            // Log or handle errors as needed.
            throw new Exception("Failed to create seeded manager account: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

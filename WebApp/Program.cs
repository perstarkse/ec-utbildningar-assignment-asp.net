using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ShowcaseService>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoleSeeder>();
builder.Services.AddScoped<UserService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(x => 
{
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlProducts")));
builder.Services.AddDbContext<IdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlAccounts")));
builder.Services.AddDbContext<ContactContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlContacts")));

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
    await roleSeeder.SeedRoles();
}
// Function above seeds user roles.

app.Run();

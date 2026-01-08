using LaboratorAnalize.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LaboratorAnalizeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LaboratorAnalizeContext")
        ?? throw new InvalidOperationException("Connection string 'LaboratorAnalizeContext' not found.")));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LaboratorAnalizeContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    // Doar Admin
    options.Conventions.AuthorizeFolder("/Pacienti", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/BuletineAnaliza");
    options.Conventions.AuthorizeFolder("/RezultateAnalize");

  
    options.Conventions.AuthorizeFolder("/Programari");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Roluri
    string[] roles = { "Admin", "User" };
    foreach (var r in roles)
        if (!await roleManager.RoleExistsAsync(r))
            await roleManager.CreateAsync(new IdentityRole(r));

    // Admin default (schimbă cum vrei)
    var adminEmail = "admin@lab.local";
    var adminPass = "Admin123!";

    var admin = await userManager.FindByEmailAsync(adminEmail);
    if (admin == null)
    {
        admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(admin, adminPass);
    }

    if (!await userManager.IsInRoleAsync(admin, "Admin"))
        await userManager.AddToRoleAsync(admin, "Admin");
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapRazorPages();

app.Run();

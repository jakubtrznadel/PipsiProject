using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Amora.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AmoraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmoraContext") ?? throw new InvalidOperationException("Connection string 'AmoraContext' not found.")));

builder.Services.AddControllersWithViews();
builder.Services.AddSession(); 

var app = builder.Build();

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

app.UseSession(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

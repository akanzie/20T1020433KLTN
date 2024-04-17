using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddMvcOptions(option =>
{
    option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddHttpClient();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(60);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
builder.Services.AddAutoMapperSetup();

builder.Services.AddCustomizeDatabase();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddHttpContextAccessor();

DatabaseConfig.Initialize(builder.Configuration.GetConnectionString("SQLServerConnectionString"));

FileConfig.Initialize(builder.Configuration.GetSection(FileConfig.FILE_STORAGE_PATHS)["ServerStoragePath"]);

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapAreaControllerRoute(
    name: "Student",
    areaName: "Student",
    pattern: "Student/{controller=Home}/{action=Index}/{id?}"
    );
app.MapAreaControllerRoute(
    name: "Teacher",
    areaName: "Teacher",
    pattern: "Teacher/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
ApplicationContext.Configure
(
    httpContextAccessor: app.Services.GetRequiredService<IHttpContextAccessor>(),
    hostEnvironment: app.Services.GetService<IWebHostEnvironment>()
);
app.Run();


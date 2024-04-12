using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Configuration;
using System.Configuration;

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
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
builder.Services.AddMvcCore();
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

app.MapControllerRoute(
    name: "Student",     
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Teacher",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
ApplicationContext.Configure
(
    httpContextAccessor: app.Services.GetRequiredService<IHttpContextAccessor>(),
    hostEnvironment: app.Services.GetService<IWebHostEnvironment>()
);
app.Run();


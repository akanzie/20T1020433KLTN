using KLTN20T1020433.Web.AppCodes;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddMvcOptions(option =>
{
    option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}); ;
builder.Services.AddHttpClient();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(60);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StudentHome}/{action=Index}/{id?}");
ApplicationContext.Configure
(
    httpContextAccessor: app.Services.GetRequiredService<IHttpContextAccessor>(),
    hostEnvironment: app.Services.GetService<IWebHostEnvironment>()
);

string connectionString = @"server=KITHGG;user id=sa;password=1;database=QuanLyBaiThi;TrustServerCertificate=true";
KLTN20T1020433.BusinessLayers.Configuration.Initialize(connectionString);
app.Run();

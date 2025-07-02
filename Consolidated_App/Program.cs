using Consolidated_App;
using Consolidated_App.AuthNavigation;
using Consolidated_App.Common.AuthNavigation;
using Consolidated_App.Models;
using Consolidated_App.WebConstants;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();  // Allow PUT, POST, DELETE, etc.
    });
});

//builder.Services.AddRazorPages()
//    .AddRazorRuntimeCompilation();

// Use Serilog for logging
builder.Logging.ClearProviders();


builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var configuration = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .Build();
var baseUrl = configuration.GetValue<string>("HeinkenBaseUrl");
//var apiKey = builder.Configuration["ApiSettings:ApiKey"];
//var apiVersion = builder.Configuration["ApiSettings:ApiVersion"];`

builder.Services.AddHttpClient("mainclient", client =>
{
    client.BaseAddress = new Uri(baseUrl);
    //client.DefaultRequestHeaders.Add("x-Api-Key", apiKey);
    //client.DefaultRequestHeaders.Add("x-client-version", apiVersion);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (messag, cart, chain, errors) => true,
    };
});
builder.Services.Configure<OtherSettings>(configuration.GetSection("OtherSettings"));
builder.Services.AddScoped<NavigatorManager>();
builder.Services.AddScoped<InitialBufferPopulator>();
builder.Services.AddScoped<SharedHelpers>();
builder.Services.AddScoped<AuthenticationManager>();
builder.Services.Configure<FormOptions>(x =>
{
    x.BufferBody = false;
    x.KeyLengthLimit = 3072; // 2 KiB  2048fff
    x.ValueLengthLimit = 4196352; // 32 MiB 4194 304
    x.ValueCountLimit = 3072;// 1024
    x.MultipartHeadersCountLimit = 32; // 16
    x.MultipartHeadersLengthLimit = 32768; // 16384
    x.MultipartBoundaryLengthLimit = 256; // 128
    x.MultipartBodyLengthLimit = 134225920; // 128 MiB  1342 17728

});

builder.Services.AddAuthentication(CNET_WebConstantes.CookieScheme)
     .AddCookie(CNET_WebConstantes.CookieScheme, options =>
     {
         options.AccessDeniedPath = "/account/denied";
         options.LoginPath = "/login";
     });
builder.Services.AddSession();

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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine("C:\\inetpub\\wwwroot\\SharedFiles", "node_modules")),
    RequestPath = "/node_modules"
});
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

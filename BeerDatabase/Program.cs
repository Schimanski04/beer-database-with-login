using BeerDatabase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireUppercase = false;
    }
    )
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator")); // má roli
});

// Prihlaseni pomoci uctu tretich stran
builder.Services.AddAuthentication()
   //.AddGoogle(options =>
   //{
   //    IConfigurationSection googleAuthNSection =
   //    config.GetSection("Authentication:Google");
   //    options.ClientId = googleAuthNSection["ClientId"];
   //    options.ClientSecret = googleAuthNSection["ClientSecret"];
   //})
   //.AddFacebook(options =>
   //{
   //    IConfigurationSection FBAuthNSection =
   //    config.GetSection("Authentication:FB");
   //    options.ClientId = FBAuthNSection["ClientId"];
   //    options.ClientSecret = FBAuthNSection["ClientSecret"];
   //})
   .AddMicrosoftAccount(microsoftOptions =>
   {
       microsoftOptions.ClientId = "33751154-5532-4296-b56d-3c193ea092a8";
       microsoftOptions.ClientSecret = "HuL8Q~DMc2E-6LzTWDJv3NFvKLLgdwNGht2ANali";
   });
    //.AddTwitter(twitterOptions =>
    //{
    //    twitterOptions.ConsumerKey = config["Authentication:Twitter:ConsumerAPIKey"];
    //    twitterOptions.ConsumerSecret = config["Authentication:Twitter:ConsumerSecret"];
    //    twitterOptions.RetrieveUserDetails = true;
    //});

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();

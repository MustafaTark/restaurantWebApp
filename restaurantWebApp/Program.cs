using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using restaurantWebApp_DAL.Contracts;
using restaurantWebApp_DAL.Data;
using restaurantWebApp_DAL.Models;
using restaurantWebApp_DAL.Repo;
using AutoMapper;
using restaurantWebApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IRepositoryBase<Meal>, MealRepositery>();
builder.Services.AddScoped<IRepositoryBase<Category>, CategoryRepositery>();
builder.Services.AddScoped<IRepositoryBase<Cart>, CartRepositery>();
builder.Services.AddScoped<IRepositoryBase<Order>, OrderRepositery>();
//builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
var builderIdentity = builder.Services.AddIdentityCore<Customer>(o => {
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 10;
    o.User.RequireUniqueEmail = true;

});
builderIdentity = new IdentityBuilder(
    builderIdentity.UserType,
    typeof(IdentityRole), builder.Services);
builderIdentity.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
var jwtSettings =builder.Configuration.GetSection("JwtSettings");
var secretKey = Environment.GetEnvironmentVariable("SECRET");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{ options.TokenValidationParameters = new TokenValidationParameters
{ ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
    ValidAudience = jwtSettings.GetSection("validAudience").Value,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)) };
});
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Restaurant");
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
   } );
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
//builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
.AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
app.MapRazorPages();

app.Run();

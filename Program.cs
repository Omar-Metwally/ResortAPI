
using WebApplication5.Services.Apartments;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using Microsoft.AspNetCore.Identity;
using WebApplication5.Models.DominModels.Auth;
using WebApplication5.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication5.Services.Auth;
using WebApplication5.Services.Owners;
using System.Text.Json.Serialization;
using WebApplication5.Services.Expenses;
using WebApplication5.Services.Bills;
using WebApplication5.Services.Leases;
using WebApplication5.Services.Newss;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDBContext>();

builder.Services.AddDbContext<DataDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataDB")
    ));
builder.Services.AddDbContext<AuthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB"))
);

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IApartmentService, ApartmentService>();

builder.Services.AddScoped<IOwnerService, OwnerService>();

builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped<IBillService, BillService>();

builder.Services.AddScoped<ILeaseService, LeaseService>();

builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();

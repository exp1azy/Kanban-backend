using KanbanBackend.Data;
using KanbanBackend.Services;
using KanbanBackend.Startup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>();

builder.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<BoardService>();
builder.Services.AddTransient<BoardColumnService>();
builder.Services.AddTransient<CardService>();

var config = builder.Configuration.GetSection("JWT");

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = config["Issuer"],
        ValidateAudience = true,
        ValidAudience = config["Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Key"])),
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
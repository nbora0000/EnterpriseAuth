using AuthenticationApi.Application.Interface;
using AuthenticationApi.Application.Service;
using AuthenticationApi.Infrastructure.CustomLogic;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsetting.json",optional:true,reloadOnChange:true)
                    .AddJsonFile($"appsetting.{builder.Environment.EnvironmentName}.json",optional:true,reloadOnChange:true);
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<SecurityJwtToken>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured."))),
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});
builder.Services.AddSwaggerGen(c=> { c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthenticationApi", Version = "v1" }); });
builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();
builder.Services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
builder.Services.AddApplicationInsightsTelemetry();
var app = builder.Build();
if(app.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
}
else{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c=>{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationApi v1");
    c.RoutePrefix = string.Empty;
});
app.Run();

using AuthenticationApi.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvConfig(builder.Environment);
builder.Services.AddControllers();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScopedServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerGenConfig();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c=>{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationApi v1");
    c.RoutePrefix = string.Empty;
});

app.Run();

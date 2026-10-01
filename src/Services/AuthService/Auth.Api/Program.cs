using Application.Interfaces;
using Auth.Api;
using Auth.Api.MiddleWares;
using Auth.Api.Seeders;
using Auth.Seeds.Options;
using Auth.Seeds.Seeders;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddScoped<ErrorHandlingMiddleWare>();
builder.Services.AddScoped<PermissionCheckerMiddleWare>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите токен без префикса 'Bearer ' — он подставится автоматически"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseMiddleware<PermissionCheckerMiddleWare>();
app.MapControllers();
await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IAuthDbContext>();
    var seedOptions = scope.ServiceProvider.GetRequiredService<IOptions<SeedOptions>>().Value;
    await db.MigrateAsync();
    try
    {
        await db.BeginTransactionAsync();
        await ObjectTypeSeeder.SeedAsync(db);
        await SuperAdminSeeder.SeedAsync(db, seedOptions);
        await db.CommitTransactionAsync();
    }
    catch
    {
        await db.RollbackTransactionAsync();
        throw;
    }
}
app.Run();
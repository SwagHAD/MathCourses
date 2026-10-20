using Math.Api;
using Microsoft.OpenApi;
using SharedKernel.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5246", "https://localhost:7284")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
// CORS первым: preflight (OPTIONS) должен получить ответ до редиректов, аутентификации и проверки доступов
app.UseCors("AllowFrontend");
// В разработке без редиректа на https: клиент ходит на http-порт, а браузер не следует редиректу
// на preflight и сбрасывает Authorization при переходе на другой origin — получалась CORS-ошибка
if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSharedKernel();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
await GeneralConfiguration.MigrateAsync(app.Services);
await app.RunAsync();
using TeraBank.Api.ExceptionHandlers;
using TeraBank.Api.Extensions;
using TeraBank.Application.Extensions;
using TeraBank.Infrastructure.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddBankDbContext(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.ConfigureAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseExceptionHandler();
await app.UseAutoMigration();

app.UseAuthEndpoints();
app.UseTransactionEndpoints();
app.UseUserEndpoints();

app.Run();
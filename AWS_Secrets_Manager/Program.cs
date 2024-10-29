using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSecretsManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/secret", async (IAmazonSecretsManager secrets) =>
{
    var request = new GetSecretValueRequest()
    {
        SecretId = "dev/myapp/connection"
    };
    var data = await secrets.GetSecretValueAsync(request);
    return Results.Ok(data.SecretString);
});

app.UseAuthorization();

app.MapControllers();

app.Run();

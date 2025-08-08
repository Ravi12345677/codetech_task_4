using Azure.AI.OpenAI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSingleton(sp =>
    new OpenAIClient(new Uri("https://<your-resource>.openai.azure.com/"),
    new AzureKeyCredential("<your-azure-openai-key>")));
builder.Services.AddSingleton<IBot, EchoBot>();

var app = builder.Build();
app.MapControllers();
app.Run();

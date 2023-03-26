using GenerateContractObjects;
using NetCore2BlocklyNew;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DocumentList>(sp =>
{
    DocumentList d = new ();
    d.InitializeFromHdd("docs");
    return d;
});
builder.Services.AddProblemDetails();
var app = builder.Build();

app.UseStatusCodePages();
app.UseExceptionHandler();
app.UseBlocklyUI(app.Environment);

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();
app.UseBlocklyAutomation();

app.Run();

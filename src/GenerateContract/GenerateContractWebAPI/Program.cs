using GenerateContractObjects;
using NetCore2BlocklyNew;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<DocumentList>(sp =>
{
    DocumentList d = new ();
    var h = sp.GetRequiredService<IWebHostEnvironment>();
    string folder = "docs";
    //if (h.IsDevelopment())
    //    folder = @"bin\Debug\net7.0\docs";

    d.InitializeFromHdd(folder,true);
    return d;
});
builder.Services.AddCors();
builder.Services.AddProblemDetails();
var app = builder.Build();

app.UseCors(it => it.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
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
app.MapFallbackToFile("{*path:nonfile}", "/index.html");

app.Run();

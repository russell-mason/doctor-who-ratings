using DoctorWhoRatings.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddRazorComponents()
       .AddInteractiveServerComponents();

builder.Services.AddSingleton<IExcelSpreadsheetReader, ExcelSpreadsheetReader>();
builder.Services.AddSingleton<IDoctorWhoDataReader, DoctorWhoDataReader>();
builder.Services.AddSingleton<IDoctorWhoDataProvider, DoctorWhoDataProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Cert removed from website, currently only supports HTTP
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

// TODO: Remove - for manual testing only
var dataProvider = app.Services.GetRequiredService<IDoctorWhoDataProvider>();
var dataSet = dataProvider.DoctorWhoData;

app.Run();

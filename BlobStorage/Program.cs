using Azure.Storage.Blobs;
using BlobStorage.Data.EF;
using BlobStorage.Logica;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<BlobStorageContext>();
builder.Services.AddScoped<IServiceLogAlbum, ServiceLogAlbum>();
builder.Services.AddSingleton(new BlobServiceClient(builder.Configuration.GetConnectionString("BlobStorage")));
builder.Services.AddScoped<IServiceBlobStorage,ServiceBlobStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BlobStorage}/{action=Index}/{id?}");

app.Run();

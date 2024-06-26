using ShoppingList.Core;
using ShoppingList.Web.Helper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IMvcBuilder mvcBuilder = builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

DataStoreOptions dataStoreOptions = builder.Configuration.GetSection(DataStoreOptions.SectionKey).Get<DataStoreOptions>();
builder.Services.AddSingleton<IDataStoreOptions>(dataStoreOptions);
builder.Services.AddDataAccess(dataStoreOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();

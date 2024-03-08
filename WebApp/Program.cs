using WebApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
    );

builder.Services.AddTransient<ClientesController, ClientesController>();
builder.Services.AddTransient<MeserosController, MeserosController>();
builder.Services.AddTransient<MesasController, MesasController>();
builder.Services.AddTransient<SupervisoresController, SupervisoresController>();
builder.Services.AddTransient<FacturasController, FacturasController>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

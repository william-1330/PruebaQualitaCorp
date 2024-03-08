using CapaDatos;
using CapaDatos.DA;
using CapaNegocio.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
    );


builder.Services.AddDbContext<TiendaContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("AppConnection"))
);

//builder.Services.AddScoped<TiendaContext>();
builder.Services.AddScoped<FacturasController>();
builder.Services.AddScoped<FacturaDA>();
builder.Services.AddScoped<ClientesController>();
builder.Services.AddScoped<ClienteDA>();
builder.Services.AddScoped<MeserosController>();
builder.Services.AddScoped<MeseroDA>();
builder.Services.AddScoped<MesasController>();
builder.Services.AddScoped<MesaDA>();
builder.Services.AddScoped<SupervisoresController>();
builder.Services.AddScoped<SupervisorDA>();
builder.Services.AddScoped<DetalleXFacturasController>();
builder.Services.AddScoped<DetalleXFacturaDA>();
builder.Services.AddScoped<ReportesController>();
builder.Services.AddScoped<ReporteDA>();

var app = builder.Build();


//using (var scope =
//  app.Services.CreateScope())
//using (var context = scope.ServiceProvider.GetService<TiendaContext>())
//    context.Database.Migrate();


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

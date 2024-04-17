using CapaDatos;
using CapaDatos.DA;
using CapaDatos.DTO;
using CapaNegocio.Controllers;
using CapaNegocio.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
    );

builder.Services.AddDbContext<TiendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionSQLServer"))
);

builder.Services.AddScoped<FacturaController>();
builder.Services.AddScoped<FacturaDA>();
builder.Services.AddScoped<ClienteController>();
builder.Services.AddScoped<ClienteDA>();
builder.Services.AddScoped<MesaController>();
builder.Services.AddScoped<MesaDA>();
//builder.Services.AddScoped<DetalleXFacturasController>();
//builder.Services.AddScoped<DetalleXFacturaDA>();
builder.Services.AddScoped<ReporteController>();
builder.Services.AddScoped<ReporteDA>();
builder.Services.AddScoped<UsuarioController>();
builder.Services.AddScoped<UsuarioDA>();
builder.Services.AddScoped<RolController>();
builder.Services.AddScoped<RolDA>();
builder.Services.AddScoped<ProductoController>();
builder.Services.AddScoped<ProductoDA>();
builder.Services.AddScoped<DashboardController>();
builder.Services.AddScoped<DashBoardDA>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica", app => {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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

app.UseCors("NuevaPolitica");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

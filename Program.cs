// 1. Usings to work with EntityFramework
using API_PROJECT_FP_DAM.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var AllowLocalhostOrigins = "AllowLocalhostOrigins";

// Nuevo servicio que es para WEB
builder.Services.AddControllersWithViews();

// 2. Conexion con bbdd
const string CONNECTIONNAME = "API_PROJECT_FP_DAM";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);




// 3. Las bases de datos van aqui
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));






var proveedor = builder.Services.BuildServiceProvider();
var configuration = proveedor.GetService<IConfiguration>();
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

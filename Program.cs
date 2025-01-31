using blog_BackEnd.Data;
using blog_BackEnd.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Slug>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Servindo arquivos estaticos de dois diretórios diferentes
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"ImgDataEditor")),
    RequestPath =  "/ImgDataEditor"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"ImgData")),
    RequestPath =  "/ImgData"
});

app.UseAuthorization();

app.MapControllers();

app.UseCors( opt => {
    opt.AllowAnyOrigin();
    opt.WithOrigins("http://localhost:5173");
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    
});

app.Run();

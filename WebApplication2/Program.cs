using Microsoft.EntityFrameworkCore;
using Project.Controller.Services;
using Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerService, CustomerService>();//“”т вс≥ додати
builder.Services.AddScoped<IEmployeesService,  EmployeesServise>();
//builder.Services.AddScoped<IAccountServices, AccountService>();
//Here changes
builder.Services.AddDbContext <BankContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

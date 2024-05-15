using EmployeeManagement.Core.Services;
using EmployeeManagement.Database.Context;
using EmployeeManagement.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.MigrationsAssembly("EmployeeManagement.Api")));
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(EmployeeManagement.Core.Mappings.AutoMapperProfile).Assembly);
builder.Services.AddControllers();

builder.Services.AddScoped<AuthorityRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<RoleRepository>();

builder.Services.AddScoped<AuthorityService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
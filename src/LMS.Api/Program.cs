using LMS.Api.Common;
using LMS.Infrastructure;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using LMS.Application.Interface;
using LMS.Application;
using LMS.Application.PipelineBehavior;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr"));
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure();

// builder.Services.AddMediator(options =>
// {
//     //options.Namespace = "LMS.Application";
//     //options.ServiceLifetime = ServiceLifetime.Transient;
// });


builder.Services.AddApplication();




var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS.Api v1"));
}

app.UseHttpsRedirection();
app.UseFluentValidationExceptionHandler();

app.UseAuthorization();

app.MapControllers();



app.Run();

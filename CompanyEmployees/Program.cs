using CompanyEmployees;
using CompanyEmployees.Extensions;
using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    // config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
})
// .AddXmlDataContractSerializerFormatters()
.AddNewtonsoftJson()
  .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
//   .AddCustomCSVFormatter()

// suppressing a default model state validation that is implemented
// due to the existence of the [ApiController] attribute in all API controllers.
// builder.Services.Configure<ApiBehaviorOptions>(options =>
// {
//     options.SuppressModelStateInvalidFilter = true;
// });

builder.Services.ConfigureSqlContext(builder.Configuration);

var app = builder.Build();
// var logger = app.Services.GetRequiredService<ILoggerManager>();
// app.ConfigureExceptionHandler(logger);
app.UseExceptionHandler(opt => { });

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
//     new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
//     .Services.BuildServiceProvider()
//     .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
//     .OfType<NewtonsoftJsonPatchInputFormatter>().First();

using Microsoft.EntityFrameworkCore;
using SchedulerCrudRazor.Models;
using Newtonsoft.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc().AddNewtonsoftJson(x => { x.SerializerSettings.ContractResolver = new DefaultContractResolver(); })
    .AddNewtonsoftJson(x => x.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat)
    .AddNewtonsoftJson(x => x.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local);
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");


var connectionString = builder.Configuration.GetConnectionString("ScheduleDataConnection");
builder.Services.AddDbContext<ScheduleDataContext>(opts => opts.UseSqlServer(connectionString));


// Add services to the container.
builder.Services.AddRazorPages();

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

app.Run();

using MicroService.Consumer.DataAccess;
using MicroService.Consumer.WebApp.Components;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Consumer.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<ConsumerContext>(options
                =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConsumerDb"));
            }, ServiceLifetime.Singleton);
            
            builder.Services.AddQuickGridEntityFrameworkAdapter();

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
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}

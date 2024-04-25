using MassTransit;
using MicroService.Producer.DataAccess;
using MicroService.Producer.WebApp.Components;
using MicroService.Producer.WebApp.Notes;
using MicroService.Producer.WebApp.Notes.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using Serilog.Events;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace MicroService.Producer.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSerilog((services, cfg) => cfg
                    .ReadFrom.Configuration(builder.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Debug)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Debug)
                    .WriteTo.Console(new ExpressionTemplate(
                        "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}",
                        theme: TemplateTheme.Code)))
                ;


            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped<INoteManager, NoteManager>();
            builder.Services.AddScoped<INotePublisher, NotePublisher>();

            builder.Services.AddDbContext<NoteProducerContext>(options
                =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProducerDb"));
            });

            builder.Services.AddQuickGridEntityFrameworkAdapter();

            builder.Services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<NoteProducerContext>(cfg =>
                {
                    cfg.QueryDelay = TimeSpan.FromSeconds(1);
                    cfg.UseSqlServer();
                    cfg.UseBusOutbox();
                });
                x.AddRider(rider =>
                {
                    rider.AddProducer<NoteCreated>("note-created");
                    rider.UsingKafka((context, cfg) =>
                    {
                        cfg.ClientId = "producer-webapp";
                        cfg.Host("localhost:19092");
                    });
                });
            });


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
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
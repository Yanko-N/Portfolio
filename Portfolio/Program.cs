using Application.Classes;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services.JsonBased;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
namespace Portfolio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IManifestClient, ManifestClient>();

            //Repositories
            builder.Services.AddTransient<IPersonalInfoService, PersonalInfoJsonRepository>();
            builder.Services.AddTransient<ISocialsService, SocialsInfoJsonRepository>();
            builder.Services.AddTransient<IHobbiesService, HobbiesInfoJsonRepository>();
            builder.Services.AddTransient<ISkillsService, SkillsInfoJsonRepository>();
            builder.Services.AddTransient<IWorkService,WorkInfoJsonRepository>();

            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
        }
    }
}

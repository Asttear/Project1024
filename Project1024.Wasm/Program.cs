using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project1024.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Dark = true;
        theme.Themes.Dark.Primary = "#FFC107";
        theme.Themes.Dark.Secondary = "#424242";
        theme.Themes.Dark.Accent = "#FFFF00";
        theme.Themes.Dark.Info = "#FFC107";
    });
});

await builder.Build().RunAsync();

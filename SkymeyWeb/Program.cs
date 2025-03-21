using SkymeyWeb.Components;


    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
var app = builder.Build();

    // Configure the HTTP request pipeline

    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();


    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAntiforgery();


    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    app.Run();

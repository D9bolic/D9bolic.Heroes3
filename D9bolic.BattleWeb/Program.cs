using D9bolic.Assets;
using D9bolic.BattleWeb.Auth;
using D9bolic.BattleWeb.Components;
using D9bolic.BattleWeb.Data;
using D9bolic.BattleWeb.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSignalR();
builder.Services.AddFilesystemImageStore(builder.Configuration);

builder.Services.AddDbContext<BattleWebDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BattleWebIdentity")
                      ?? "Data Source=battleweb-identity.db"));

builder.Services
    .AddIdentity<BattleWebUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<BattleWebDbContext>()
    .AddDefaultTokenProviders();

// AddIdentity registers cookie authentication; configure cookie behavior here.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/login";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.HttpOnly = true;
});

// Microsoft account is the default external provider (chosen because it ships in
// Microsoft.AspNetCore.Authentication.MicrosoftAccount with no extra runtime deps).
// Wired only when both ClientId and ClientSecret are present so dev environments
// without a real registration fall back cleanly to Identity local accounts.
var msClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
var msClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
if (!string.IsNullOrWhiteSpace(msClientId) && !string.IsNullOrWhiteSpace(msClientSecret))
{
    builder.Services.AddAuthentication()
        .AddMicrosoftAccount(options =>
        {
            options.ClientId = msClientId;
            options.ClientSecret = msClientSecret;
        });
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapAccountEndpoints();
app.MapHub<BattleHub>("/hubs/battle");
app.MapImageStore();

// Apply migrations / ensure DB exists for the local-account fallback so the
// flow works end-to-end out of the box in dev.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BattleWebDbContext>();
    db.Database.EnsureCreated();
}

app.Run();

using D9bolic.BattleWeb.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace D9bolic.BattleWeb.Auth;

/// <summary>
/// Minimal login/logout endpoints. The default route is "Identity local accounts"
/// so the flow is end-to-end testable without any external provider credentials.
/// When an external provider is registered (see <c>Program.cs</c>), pass its scheme
/// name as <c>?provider=Microsoft</c> on /account/login to start the OAuth challenge.
/// </summary>
public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/account").WithTags("Account");

        // GET /account/login — interactive form for local accounts; also kicks off
        //   external OAuth when ?provider=<scheme> is present.
        group.MapGet("/login", (HttpRequest req, [FromQuery] string? provider, [FromQuery] string? returnUrl) =>
        {
            if (!string.IsNullOrWhiteSpace(provider))
            {
                var props = new AuthenticationProperties { RedirectUri = returnUrl ?? "/" };
                return Results.Challenge(props, [provider]);
            }
            return Results.Content(LoginPage(returnUrl), "text/html");
        });

        // POST /account/login — local-account sign-in (Identity).
        group.MapPost("/login", async (
            [FromForm] string username,
            [FromForm] string password,
            [FromForm] string? returnUrl,
            SignInManager<BattleWebUser> signInManager,
            UserManager<BattleWebUser> userManager) =>
        {
            // Lazy-create the account if it doesn't exist — this is the dev convenience
            // path that lets a fresh checkout run end-to-end without a registration step.
            var user = await userManager.FindByNameAsync(username);
            if (user is null)
            {
                user = new BattleWebUser { UserName = username, Email = username };
                var create = await userManager.CreateAsync(user, password);
                if (!create.Succeeded)
                {
                    return Results.Content(LoginPage(returnUrl, "Could not create account: "
                        + string.Join("; ", create.Errors.Select(e => e.Description))), "text/html");
                }
            }

            var result = await signInManager.PasswordSignInAsync(username, password, isPersistent: true, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Results.Content(LoginPage(returnUrl, "Invalid username or password."), "text/html");
            }
            return Results.Redirect(returnUrl ?? "/");
        }).DisableAntiforgery();

        // POST /account/logout — sign out and redirect home.
        group.MapPost("/logout", async (SignInManager<BattleWebUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Redirect("/");
        }).DisableAntiforgery();

        // GET /account/logout — convenience GET for browser bookmarks during dev.
        group.MapGet("/logout", async (HttpContext ctx) =>
        {
            await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Results.Redirect("/");
        });

        return routes;
    }

    private static string LoginPage(string? returnUrl, string? error = null)
    {
        var ret = System.Net.WebUtility.HtmlEncode(returnUrl ?? "/");
        var err = string.IsNullOrEmpty(error)
            ? string.Empty
            : $"<p style=\"color:#c00\">{System.Net.WebUtility.HtmlEncode(error)}</p>";
        return $"""
            <!doctype html>
            <html><head><meta charset="utf-8"><title>Sign in — BattleWeb</title></head>
            <body style="font-family:sans-serif;max-width:480px;margin:64px auto">
              <h1>Sign in</h1>
              {err}
              <form method="post" action="/account/login">
                <input type="hidden" name="returnUrl" value="{ret}" />
                <p><label>Username <input name="username" required /></label></p>
                <p><label>Password <input type="password" name="password" required /></label></p>
                <p><button type="submit">Sign in (local account)</button></p>
              </form>
              <p>Or sign in with <a href="/account/login?provider=Microsoft&amp;returnUrl={ret}">Microsoft</a> (only available when configured).</p>
            </body></html>
            """;
    }
}

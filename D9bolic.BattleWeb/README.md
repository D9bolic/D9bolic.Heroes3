# D9bolic.BattleWeb

Blazor Server host for the Heroes.Battle engine. Exposes a SignalR hub at
`/hubs/battle` for real-time client interaction with battle state.

## Authentication

The host uses ASP.NET Core cookie authentication with two parallel sign-in paths:

| Path | Mechanism | When it's used |
| --- | --- | --- |
| **Local accounts** | ASP.NET Core Identity backed by SQLite (`battleweb-identity.db`) | Always available — fallback for dev environments without external provider credentials |
| **Microsoft account** | `Microsoft.AspNetCore.Authentication.MicrosoftAccount` | Only registered when `Authentication:Microsoft:ClientId` *and* `Authentication:Microsoft:ClientSecret` are present in configuration |

`[Authorize]` on `BattleHub` means unauthenticated SignalR negotiate requests to
`/hubs/battle` are rejected by the cookie middleware before the hub is hit.
Authenticated clients are visible to hub code via `Hub.Context.UserIdentifier`,
which Identity populates from the user's `NameIdentifier` claim.

### Why Microsoft account as the default external provider

Picked because it ships in-box with `Microsoft.AspNetCore.Authentication.MicrosoftAccount`
and registers an OAuth 2.0 client with no extra runtime dependencies — the lightest
external provider to add. The full external-provider list (Google, Facebook, Twitter,
GitHub via OAuth handlers, OpenIddict for self-hosted IdPs, etc.) is one provider
swap away — see below.

### Endpoints

| Verb | Route | Purpose |
| --- | --- | --- |
| GET | `/account/login` | HTML form for local-account sign-in. Add `?provider=Microsoft` to start an external OAuth challenge. |
| POST | `/account/login` | Submit local-account credentials (lazy-creates the account on first sign-in for dev convenience). |
| POST | `/account/logout` | Sign out, redirect to `/`. |
| GET | `/account/logout` | Convenience GET that does the same — bookmarkable during dev. |

### Local-dev quick start

1. `dotnet run` (project: `D9bolic.BattleWeb`)
2. Browse to `http://localhost:5077/account/login`.
3. Type any username/password — the account is created on first sign-in. The
   cookie is set; you can now connect to `/hubs/battle`.
4. To unwire the lazy account creation for production, delete the
   `userManager.CreateAsync(...)` block in `Auth/AccountEndpoints.cs`.

### Swapping the external provider

Provider wiring lives in `Program.cs`:

```csharp
var msClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
var msClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
if (!string.IsNullOrWhiteSpace(msClientId) && !string.IsNullOrWhiteSpace(msClientSecret))
{
    builder.Services.AddAuthentication()
        .AddMicrosoftAccount(options => { /* ... */ });
}
```

To swap providers:

1. Replace the `Microsoft.AspNetCore.Authentication.MicrosoftAccount` package in
   `D9bolic.BattleWeb.csproj` with the target provider's package, e.g.
   `Microsoft.AspNetCore.Authentication.Google`.
2. Replace `.AddMicrosoftAccount(...)` with the equivalent extension
   (`.AddGoogle(...)`, `.AddOpenIdConnect(...)`, etc.).
3. Update the configuration keys (e.g.
   `Authentication:Google:ClientId` / `:ClientSecret`).
4. Update the link on the login page in `Auth/AccountEndpoints.cs` —
   `?provider=Microsoft` becomes `?provider=Google` (or whatever scheme name the
   handler registers).

### Configuration

Set provider credentials via standard ASP.NET Core configuration — typically
`appsettings.Development.json` or user-secrets:

```json
{
  "Authentication": {
    "Microsoft": {
      "ClientId":     "<client-id>",
      "ClientSecret": "<client-secret>"
    }
  },
  "ConnectionStrings": {
    "BattleWebIdentity": "Data Source=battleweb-identity.db"
  }
}
```

When no `ClientId`/`ClientSecret` is configured the external provider is simply
not registered and the local-account flow is the only path.

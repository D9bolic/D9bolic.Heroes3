using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace D9bolic.BattleWeb.Hubs;

[Authorize]
public partial class BattleHub : Hub
{
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace D9bolic.BattleWeb.Data;

public class BattleWebDbContext(DbContextOptions<BattleWebDbContext> options)
    : IdentityDbContext<BattleWebUser>(options);

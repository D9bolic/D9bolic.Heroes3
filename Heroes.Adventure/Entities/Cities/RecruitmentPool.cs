using Heroes.Adventure.Entities.Resources;

namespace Heroes.Adventure.Entities.Cities;

public class RecruitmentPool
{
    public string UnitName { get; init; } = string.Empty;

    public int Available { get; set; }

    public int GrowthPerWeek { get; init; }

    public Dictionary<ResourceType, int> CostPerUnit { get; init; } = [];
}

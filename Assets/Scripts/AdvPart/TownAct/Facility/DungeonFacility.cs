using System.Collections.Generic;

public class Dungeon : Facility
{
    public Dungeon()
    {
        Name = "ダンジョン";
        FacilityActs = new List<FacilityAct>()
        {
            new Dungeon_Quest()
        };
    }
}

public class Dungeon_Quest : FacilityAct
{
    public Dungeon_Quest() { Name = "探索"; }
}
using System.Collections.Generic;
using MyUtility;
using UnityEngine;

public class Atelier : Facility
{
    public Atelier()
    {
        Name = "アトリエ";

        FacilityActs = new List<FacilityAct>()
        {
            new Ateller_Storage(), new Atelier_Alchemy(), new Atelier_Rest(), new Atelier_Talk()
        };
    }
}

public class Ateller_Storage : FacilityAct
{
    public Ateller_Storage() { Name = "収納"; }

    public override void Action()
    {
        AdvPartManager.Instance.startUpAtelierStorage();
    }
}

public class Atelier_Alchemy : FacilityAct
{
    public Atelier_Alchemy() { Name = "錬成"; }
}

public class Atelier_Rest : FacilityAct
{
    public Atelier_Rest() { Name = "休む"; }

    public override void Action()
    {
        int value = 20;
        var playerData = PlayerDataManager.Instance;
        Util.Range(ref playerData.Stamina, 0, playerData.MaxStamina, value);
        AdvUIManager.Instance.UpdateText();
        FinishDay();
    }
}

public class Atelier_Talk : FacilityAct
{
    public Atelier_Talk() { Name = "会話"; }

    public override void Action()
    {
        AdvPartManager.Instance.StartUpSingleText("Texts/Attelier_Talk");
    }
}

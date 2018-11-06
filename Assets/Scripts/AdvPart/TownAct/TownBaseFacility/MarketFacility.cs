using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class TownBaseAct : AbstractTownAct
{
    public class Market : Facility
    {
        public Market()
        {
            Name = "マーケット";
            FacilityActs = new List<FacilityAct>()
            {
                new Market_Purchase(), new Market_Sale(), new Market_Talk()
            };
        }
    }

    public class Market_Purchase : FacilityAct
    {
        public Market_Purchase() { Name = "購入"; }

        public override void Action()
        {
            AdvPartManager.Instance.StartUpMarketPurchase();
        }
    }

    public class Market_Sale : FacilityAct
    {
        public Market_Sale() { Name = "売却"; }

        public override void Action()
        {
            AdvPartManager.Instance.StartUpMarketSell();
        }
    }

    public class Market_Talk : FacilityAct
    {
        public Market_Talk() { Name = "会話"; }
    }
}

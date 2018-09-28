using System.Collections.Generic;

public class Coffee : Facility
{
    public Coffee()
    {
        Name = "コーヒーハウス";

        FacilityActs = new List<FacilityAct>()
        {
            new Coffee_Mission(), new Coffee_Request(), new Coffee_Info(), new Coffee_Talk()
        };

        welcomeFirst.Add(new TextBox() { Person = "マスター", Message = "いらっしゃい" });
        welcomeNext.Add(new TextBox() { Person = "マスター", Message = "忘れ物かい？" });
    }
}

public class Coffee_Mission : FacilityAct
{
    public Coffee_Mission() { Name = "依頼"; }
}

public class Coffee_Request : FacilityAct
{
    public Coffee_Request() { Name = "外注"; }

    public override void Action()
    {
        AdvPartManager.Instance.StartUpCoffeeRequest();
    }
}

public class Coffee_Info : FacilityAct
{
    public Coffee_Info() { Name = "情報"; }

    public override void Action()
    {
        AdvPartManager.Instance.StartUpMultiText("Texts/Coffee_Info");
    }
}

public class Coffee_Talk : FacilityAct
{
    public Coffee_Talk() { Name = "会話"; }
}

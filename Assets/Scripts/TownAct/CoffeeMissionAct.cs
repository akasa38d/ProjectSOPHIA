using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMissionAct : AbstractSecondTownAct<CoffeeMissionAct>
{
    VerticalSelectLong verticalSelectBoxes;

    SingleIterator iterator;

    public CoffeeMissionAct(VerticalSelectLong select, Exec exec):base(select, exec)
    {
        ReturnAct = exec;
    }

    



}

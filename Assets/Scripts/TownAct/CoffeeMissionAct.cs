using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMissionAct : AbstractSecondTownAct<CoffeeMissionAct>
{
    SelectLayoutLong verticalSelectBoxes;

    SingleIterator iterator;

    public CoffeeMissionAct(SelectLayoutLong select, Exec exec):base(select, exec)
    {
        ReturnAct = exec;
    }

    



}

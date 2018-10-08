using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionData
{
    public string Name;

    public enum MissionType { ItemNum, ItemProp, ItemQual, Hunt, Fight };
    public MissionType Type;
}

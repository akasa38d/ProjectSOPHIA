using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionData
{
    public string Name;

    public enum MissionType { Battle, Item };
    public MissionType Type;

    public int Limit;
}

using System.Collections.Generic;
using UnityEngine;

//現在未使用
public class VerticalSelectLong : VerticalSelect
{
    [SerializeField]
    protected GameObject upIcon;
    public GameObject UpIcon { get { return upIcon; } }

    [SerializeField]
    protected GameObject downIcon;
    public GameObject DownIcon { get { return downIcon; } }
}

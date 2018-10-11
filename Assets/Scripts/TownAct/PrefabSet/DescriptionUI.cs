using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//現在未使用
public class DescriptionUI : ImageUI
{
    public Text Desctiption { private set; get; }

    public override void Awake()
    {
        base.Awake();
        Desctiption = transform.GetChild(0).GetComponent<Text>();
    }
}

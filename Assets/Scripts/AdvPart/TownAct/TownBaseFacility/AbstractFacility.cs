﻿using System.Collections.Generic;
using UnityEngine;

public partial class TownBaseAct : AbstractTownAct
{
    public abstract class Facility
    {
        public string Name;

        public List<FacilityAct> FacilityActs;

        public bool firstFlag = false;
        public List<TextBox> welcomeFirst = new List<TextBox>();
        public List<TextBox> welcomeNext = new List<TextBox>();

        public void Refresh() { firstFlag = false; }
    }

    public abstract class FacilityAct
    {
        public string Name = "hoge";
        public virtual void Action()
        {
            Debug.Log("未実装");
        }

        public void FinishDay()
        {
            PlayerDataManager.Instance.Day++;
            //暗くなる処理を入れたい
        }
    }
}
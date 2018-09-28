using System.Collections.Generic;
using UnityEngine;

public class RunnerBase
{
    public int ID;
    public string Name;
    public string ImageFileName;
    public List<int> NextExp = new List<int>();
    public List<int> Price = new List<int>();
    public List<int> Power = new List<int>();
    public enum Personality { Balance, Quantity, Rarity }
    public Personality Type;
    public string Description;
}

public class RunnerData
{
    RunnerBase runnerBase;  //データベースから取得
    public int LV = 1;
    public int Exp;     //セーブ事項

    public string Name { get { return runnerBase.Name; } }
    public string ImageFileNmae { get { return runnerBase.ImageFileName; } }
    public List<int> NextExp { get { return runnerBase.NextExp; } }     //重要
    public int Price { get { return runnerBase.Price[LV-1]; } }
    public int Power { get { return runnerBase.Power[LV-1]; } }
    public RunnerBase.Personality Type { get { return runnerBase.Type; } }
    public string Description { get { return runnerBase.Description; } }

    public RunnerData(int id, int exp)
    {
        runnerBase = DataBaseManager.Instance.GetRunnerBase(id);
        Exp = exp;
        CalcLV();
    }

    public void CalcLV()
    {
        if (Exp >= NextExp[0] && LV == 1) { LV = 2; }
        if (Exp >= NextExp[1] && LV == 2) { LV = 3; }
    }

}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : SingletonMonoBehaviour<Controller>
{
    //ステート 関係全部いらない

    State currentState;
    public List<State> States = new List<State>();
    public void SetState(string stateName, State.Exec update)
    {
        var state = States.Where(n => n.Name == stateName);

        if (state.Count() == 0)
        {
            States.Add(new State() { Name = stateName, ControlUpdate = update });
        }
        currentState = States.Where(n => n.Name == stateName).Last();
 //       Debug.Log(currentState.Name);
    }

    public const int CatchButtonsCount = 4;
    public bool[] CatchButtonsEnter = new bool[CatchButtonsCount];
    public bool[] CatchButtonsDown = new bool[CatchButtonsCount];

    //ステート
    public class State
    {
        //識別用の名前
        public string Name;

        //デリゲートで操作を受け取る
        public delegate void Exec();
        public Exec ControlUpdate;
    }

    public void Start()
    {
        States.Add(new State() { Name = "Idle", ControlUpdate = () => { } });
    }

    public void Update()
    {
        //        currentState.ControlUpdate();

        AdvPartManager.Instance.CurrentAct.Update();

        for (int i = 0; i < CatchButtonsCount; i++)
        {
            if (CatchButtonsEnter[i] == true)
            {
                CatchButtonsEnter[i] = false;
            }   

            if(CatchButtonsDown[i] == true)
            {
                CatchButtonsDown[i] = false;
            }
        }        
    }
}



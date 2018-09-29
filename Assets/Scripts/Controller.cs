using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : SingletonMonoBehaviour<Controller>
{
    public const int CatchButtonsCount = 4;
    public bool[] CatchButtonsEnter = new bool[CatchButtonsCount];
    public bool[] CatchButtonsDown = new bool[CatchButtonsCount];

    public void Update()
    {
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



using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : SingletonMonoBehaviour<Controller>
{
    public const int CatchButtonsCount = 4;
    public bool[] CatchButtonsEnter = new bool[CatchButtonsCount];
    public bool[] CatchButtonsDown = new bool[CatchButtonsCount];

    public delegate void Exec();
    Exec controlUpdate;
    public Exec SetControlUpdate { set { controlUpdate = value; } }

    public void Update()
    {
        controlUpdate();

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



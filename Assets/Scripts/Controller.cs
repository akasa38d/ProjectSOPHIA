using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : SingletonMonoBehaviour<Controller>
{
    public const int CatchButtonsCount = 4;
    public bool[] CatchButtonsEnter = new bool[CatchButtonsCount];
    public bool[] CatchButtonsDown = new bool[CatchButtonsCount];

    public bool CanControl = false;

    public ISceneManager CurrentManager;

    public void Update()
    {
        CurrentManager?.Control();

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log((AdvPartManager.Instance.CurrentAct?.GetType()));
        }
    }
}



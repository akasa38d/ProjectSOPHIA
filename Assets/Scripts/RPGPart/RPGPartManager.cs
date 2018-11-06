using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPartManager : SingletonMonoBehaviour<RPGPartManager>, ISceneManager
{
    void Start()
    {
        Controller.Instance.CurrentManager = this;
    }

    public void Control()
    {

    }
}

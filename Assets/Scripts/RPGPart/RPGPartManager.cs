using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPartManager : SingletonMonoBehaviour<RPGPartManager> {

    void Start()
    {
        SetControl();
    }

    void SetControl()
    {
        Controller.Instance.SetControlUpdate = () => { };
    }
}

using System.Collections.Generic;
using UnityEngine;

public class UIPrefabsSet : MonoBehaviour
{
    public string Name;

    [SerializeField]
    protected List<GameObject> buttons;
    public List<GameObject> Buttons { get { return buttons; } }

    [SerializeField]
    protected GameObject layout;

    [SerializeField]
    protected GameObject window;
    public GameObject Window { get { return window; } }

    public void Awake()
    {
        if(layout != null)
        {
            buttons = new List<GameObject>();
            foreach(Transform n in layout.transform)
            {
                buttons.Add(n.gameObject);
            }
        }        
    }
}

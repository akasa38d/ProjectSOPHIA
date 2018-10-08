using System.Collections.Generic;
using UnityEngine;

public class VerticalSelectBoxes : MonoBehaviour
{
    public string Name;

    [SerializeField]
    protected List<GameObject> buttons;
    public List<GameObject> Buttons { get { return buttons; } }

    [SerializeField]
    protected GameObject upIcon;
    public GameObject UpIcon { get { return upIcon; } }

    [SerializeField]
    protected GameObject downIcon;
    public GameObject DownIcon { get { return DownIcon; } }

    [SerializeField]
    protected GameObject layout;

    public void Awake()
    {
        if (layout != null)
        {
            buttons = new List<GameObject>();
            foreach (Transform n in layout.transform)
            {
                buttons.Add(n.gameObject);
            }
        }
    }
}

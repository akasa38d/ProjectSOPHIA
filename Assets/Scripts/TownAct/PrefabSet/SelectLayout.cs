using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLayout : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> buttons;
    public List<GameObject> Buttons { get { return buttons; } }

    [SerializeField]
    protected GameObject layout;

    public virtual void Close()
    {
        foreach (var n in Buttons)
        {
            n.SetActive(false);
            n.transform.localScale = new Vector3(1, 1, 1);
        }
    }

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

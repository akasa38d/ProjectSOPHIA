using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownBaseSet : MonoBehaviour
{
    //ボタン
    [SerializeField]
    List<GameObject> buttons;
    public List<GameObject> Buttons { get { return buttons; } }

    //施設の画像
    [SerializeField]
    public List<Sprite> facilitySprites;

    [SerializeField]
    public GameObject FacilityImage;

    [SerializeField]
    protected GameObject layout;

    [SerializeField]
    public Image BGFacility;

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

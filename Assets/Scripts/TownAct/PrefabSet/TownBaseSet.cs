using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownBaseSet : MonoBehaviour
{
    //ボタン
    [SerializeField]
    public List<GameObject> FacilityButtons;

    //施設の画像
    [SerializeField]
    public List<Sprite> facilitySprites;

    [SerializeField]
    public GameObject FacilityImage;

    [SerializeField]
    public Image BGFacility;
}

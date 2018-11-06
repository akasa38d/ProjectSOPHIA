using UnityEngine;

public class PrefabsManager : SingletonMonoBehaviour<PrefabsManager>
{
    #region ADV

    [SerializeField]
    GameObject basicSelectButton;
    public GameObject BasicSelectButton { get { return basicSelectButton; } }

    [SerializeField]
    GameObject basicSelectRight;
    public GameObject BasicSelectRight { get { return basicSelectRight; } }

    [SerializeField]
    GameObject twoTopicSelect;
    public GameObject TwoTopicSelect { get { return twoTopicSelect; } }

    [SerializeField]
    GameObject facilityImage;
    public GameObject FacilityImage { get { return facilityImage; } }

    #endregion

    #region RPG

    [SerializeField]
    GameObject plainCell;
    public GameObject PlainCell { get { return plainCell; } }

    [SerializeField]
    GameObject playerPrefab;
    public GameObject PlayerPrefab { get { return playerPrefab; } }

    [SerializeField]
    GameObject enemyPrefab;
    public GameObject EnemyPrefab { get { return enemyPrefab; } }

    #endregion

    [SerializeField]
    GameObject itemSetPrefabs;
    public GameObject ItemSetPrefabs { get { return itemSetPrefabs; } }

    [SerializeField]
    GameObject itemDescription;
    public GameObject ItemDescription { get { return itemDescription; } }

}

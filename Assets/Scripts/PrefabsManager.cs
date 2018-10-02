using UnityEngine;

public class PrefabsManager : SingletonMonoBehaviour<PrefabsManager>
{
    #region Adv

    [SerializeField]
    GameObject townBaseSet;
    public GameObject TownBaseSet { get { return townBaseSet; } }

    [SerializeField]
    GameObject townTalkSet;
    public GameObject TownTalkSet { get { return townTalkSet; } }

    [SerializeField]
    GameObject coffeeRequestSet;
    public GameObject CoffeeRequestSet { get { return coffeeRequestSet; } }

    #endregion

    [SerializeField]
    GameObject itemSetPrefabs;
    public GameObject ItemSetPrefabs { get { return itemSetPrefabs; } }

    [SerializeField]
    GameObject itemDescription;
    public GameObject ItemDescription { get { return itemDescription; } }



}

using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int BaseID { private set; get; }
    public int[] PropID { private set; get; }

    public ItemBase Base { private set; get; }
    public ItemProperty[] Prop { private set; get; }
    public bool[] PropIsActive { private set; get; }

    #region コンストラクタ

    public Item(int baseID, int propID)
    {
        BaseID = baseID;
        PropID = new int[2] { propID, 0 };
        createItem();
    }

    public Item(int baseID, int propID1, int propID2)
    {
        BaseID = baseID;
        PropID = new int[2] { propID1, propID2};
        createItem();
    }

    void createItem()
    {
        var dataBase = DataBaseManager.Instance;
        Base = dataBase.GetItemBase(BaseID);
        Prop = new ItemProperty[2] { dataBase.GetItemProp(PropID[0]), dataBase.GetItemProp(PropID[1]) };
        PropIsActive = new bool[2] { checkActive(Prop[0].Type), checkActive(Prop[1].Type) };
    }

    #endregion

    #region プロパティ
    //特性と名前合成したい
    public string Name { get { return Base.Name; } }
    public ItemBase.BaseType Type { get { return Base.Type; } }
    public List<ItemBase.ItemMaterialType> MaterialTypes { get { return Base.MaterialTypes; } }

    public int Value
    {
        get
        {
            return Base.Value
                * buff(PropIsActive[0] == true ? Prop[0].Value : 0)
                * buff(PropIsActive[1] == true ? Prop[1].Value : 0);
        }
    }
    public int Price
    {
        get
        {
            return Base.Price
                * buff(PropIsActive[0] == true ? Prop[0].Price : 0)
                * buff(PropIsActive[1] == true ? Prop[0].Price : 0);
        }
    }
    public int Quality
    {
        get
        {
            return Base.Quality
            * buff(PropIsActive[0] == true ? Prop[0].Quality : 0)
            * buff(PropIsActive[1] == true ? Prop[0].Quality : 0);
        }
    }
    public int Range
    {
        get
        {
            return Base.Quality
            * buff(PropIsActive[0] == true ? Prop[0].Range : 0)
            * buff(PropIsActive[1] == true ? Prop[0].Range : 0);
        }
    }
    public int InitNum
    {
        get
        {
            return Base.Quality
            * buff(PropIsActive[0] == true ? Prop[0].InitNum : 0)
            * buff(PropIsActive[1] == true ? Prop[0].InitNum : 0);
        }
    }

    public string Description { get { return Base.Description; } }

    #endregion

    int buff(int power) { return (power + 100) / 100; }

    bool checkActive(ItemProperty.PropType propType)
    {
        switch (propType)
        {
            case ItemProperty.PropType.Null:
                return false;

            case ItemProperty.PropType.Use:
                if (Type == ItemBase.BaseType.Use) { return true; }
                return false;

            case ItemProperty.PropType.Bomb:
                if (Type == ItemBase.BaseType.Bomb) { return true; }
                return false;

            case ItemProperty.PropType.Equip:
                if (Type == ItemBase.BaseType.Weapon ||
                   Type == ItemBase.BaseType.Armor ||
                   Type == ItemBase.BaseType.Accessories) { return true; }
                return false;

            case ItemProperty.PropType.Material:
                if (Type == ItemBase.BaseType.Material) { return true; }
                return false;

            case ItemProperty.PropType.All:
                return true;

            default:
                return false;
        }
    }
}

public class ItemBase
{
    //番号
    public int ID;

    //名前
    public string Name;

    //種類
    public BaseType Type;
    public enum BaseType { Null, Use, Bomb, Weapon, Armor, Accessories, Material, Special };

    //使用時の効果
    public ItemAction ActionType;
    public enum ItemAction { Null, Throw, Heal, Buff, Area, Floor };

    //素材タイプ
    public List<ItemMaterialType> MaterialTypes = new List<ItemMaterialType>();
    public enum ItemMaterialType { Null, Herb, Metal, Bullet };

    //効果量（品質・特性に依存）
    public int Value;

    //価格（品質・特性に依存）売値と買値分ける？
    public int Price;

    //品質（－２～２）いらない？
    public int Quality;

    //範囲（－１～１）
    public int Range;

    //数（－２～２）
    public int InitNum;

    //説明文
    public string Description;
}

public class ItemProperty
{
    public int ID;
    public string Name;

    public PropType Type;
    public enum PropType { Null, Use, Bomb, Equip, Material, All };

    public int Value;
    public int Price;
    public int Quality;
    public int Range;
    public int InitNum;
}

//直接入手するアイテムのデータ
public class ItemOrigin
{
    public int ID;
    public int BaseID { private set; get; }
    public int PropID { private set; get; }

    public ItemOrigin(int baseID, int propID)
    {
        BaseID = baseID;
        PropID = propID;
    }
}

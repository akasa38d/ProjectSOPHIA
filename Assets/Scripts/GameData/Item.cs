using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ItemBase
{
    ItemBase itemBase;
    //    public ItemProperty[] itemProperty = new ItemProperty[3];

    public Item(ItemBase _itemBase)
    {
        itemBase = _itemBase;
    }

    public new int ID { get { return itemBase.ID; } }
    public new string Name { get { return itemBase.Name; } }
    public new ItemType Type { get { return itemBase.Type; } }
    public new List<ItemMaterialType> MaterialTypes { get { return itemBase.MaterialTypes; } }
    public new int Value { get { return itemBase.Value; } }
    public new int Price { get { return itemBase.Price; } }
    public new int Quality { get { return itemBase.Quality; } }
    public new string Description { get { return itemBase.Description; } }
}

public class ItemBase
{
    //番号
    public int ID;

    //名前
    public string Name;

    //種類
    public ItemType Type;
    public enum ItemType { Null, Use, Weapon, Armor, Accessories, Material, Special };

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

    //品質
    public int Quality;

    //数（変動）
    public int Num;

    //説明文
    public string Description;
}

/*
public class ItemProperty
{
    
}
*/

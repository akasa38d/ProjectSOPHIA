using MyUtility;
using UnityEngine;

public class TownBaseIterator
{
    int maxFacility { get { return maxFacilityAct.Length; } }
    int[] maxFacilityAct;

    public int Facility;

    int tempFacility = 0;
    public int TempFacility
    {
        set
        {
            tempFacility = value;
            tempFacility = Util.Roop(tempFacility, 0, maxFacility - 1);
        }
        get { return tempFacility; }
    }

    int facilityAct = 0;
    public int FacilityAct
    {
        set
        {
            facilityAct = value;
            facilityAct = Util.Roop(facilityAct, 0, maxFacilityAct[Facility] - 1);
        }
        get { return facilityAct; }
    }

    public TownBaseIterator(int[] array)
    {
        maxFacilityAct = new int[array.Length];
        for (int i = 0; i < maxFacilityAct.Length; i++)
        {
            maxFacilityAct[i] = array[i];
        }
    }

    public void ResetFacilityAct()
    {
        facilityAct = 0;
    }
}

public class SingleIterator
{
    public int MaxNumber { private set; get; }

    int num = 0;
    public int Num
    {
        set
        {
            num = value;
            num = Util.Roop(num, 0, MaxNumber - 1);
        }

        get { return num; }
    }

    public SingleIterator(int maxNum)
    {
        MaxNumber = maxNum;
    }

    public void ResetNum()
    {
        num = 0;
    }
}

public class ItemIterator<T>
{
    //内部のアイテム・キャンセル
    public int InnerNow { get { return UINow + SlideNow * ColumnCount; } }
    public int InnerMax { private set; get; }

    //カーソルの位置
    public int UINow { private set; get; }
    public int UIMax { get { return InnerMax - SlideNow * ColumnCount < ButtonMax ? InnerMax - SlideNow * ColumnCount : ButtonMax; } }

    //ボタンの最大数
    public int ButtonMax { private set; get; }

    //スライド
    public int SlideNow { private set; get; }
    public int SlideMax { private set; get; } = 0;

    //１行あたりの数
    public readonly int ColumnCount = 7;

    AbstractSecondItemAct<T> itemAct;

    public ItemIterator(int innerMax, int buttonMax, AbstractSecondItemAct<T> act)
    {
        itemAct = act;
        InnerMax = innerMax;
        ButtonMax = buttonMax;
        SlideMax = InnerMax < ButtonMax ? 0 : (InnerMax - ButtonMax - 1) / ColumnCount + 1;
    }

    public void ResetInnerMax(int innerMax)
    {
        InnerMax = innerMax;
    }

    public void Up()
    {
        if (InnerNow >= ColumnCount)
        {
            if (UINow >= ColumnCount) { UINow -= ColumnCount; }
            else
            {
                SlideNow--;
                itemAct.LayoutObjects();
            }
        }
    }

    public void Down()
    {
        if (InnerNow + ColumnCount < InnerMax)
        {
            if (UINow + ColumnCount < UIMax) { UINow += ColumnCount; }
            else
            {
                SlideNow++;
                itemAct.LayoutObjects();
            }
        }
        else if (SlideNow < SlideMax)
        {
            UINow -= ColumnCount;
            SlideNow++;
            itemAct.LayoutObjects();
        }
    }

    public void Left()
    {
        if (InnerNow > 0 && UINow % ColumnCount != 0)
        {
            UINow--;
        }
        /*ループ
        else
        {
            if (UINow + ColumnCount - 1 > UIMax) { UINow = UIMax - 1; }
            else { UINow += ColumnCount - 1; }
        }
        */
    }

    public void Right()
    {
        if (InnerNow + 1 < InnerMax && UINow % ColumnCount != ColumnCount - 1)
        {
            UINow++;
        }
        /*ループ
        else
        {
            UINow -= InnerNow2 % ColumnCount;
        }
        */
    }
}

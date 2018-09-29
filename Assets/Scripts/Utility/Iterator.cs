using MyUtility;

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
            Util.Roop(ref tempFacility, 0, maxFacility - 1);
        }
        get { return tempFacility; }
    }

    int facilityAct = 0;
    public int FacilityAct
    {
        set
        {
            facilityAct = value;
            Util.Roop(ref facilityAct, 0, maxFacilityAct[Facility] - 1);
        }
        get { return facilityAct; }
    }

    public TownBaseIterator(TownBaseAct townBaseAct)
    {
        maxFacilityAct = new int[townBaseAct.Facilities.Count];
        for (int i = 0; i < maxFacilityAct.Length; i++)
        {
            maxFacilityAct[i] = townBaseAct.Facilities[i].FacilityActs.Count;
        }
    }

    public void ResetFacilityAct()
    {
        facilityAct = 0;
    }
}

public class SingleIterator
{
    int maxNumber;
    public int MaxNumber { get { return maxNumber; } }

    int count = 0;
    public int Count
    {
        set
        {
            count = value;
            Util.Roop(ref count, 0, maxNumber - 1);
        }

        get { return count; }
    }

    public SingleIterator(int maxNum)
    {
        maxNumber = maxNum;
    }

    public void ResetCount()
    {
        count = 0;
    }
}

public class ItemIterator<T>
{
    //内部のアイテム・キャンセル
    public int InnerNow { private set; get; }
    public int InnerMax { private set; get; }

    //カーソルの位置
    public int UINow { private set; get; }
    public int UIMax { private set; get; }

    //ボタンの最大数
    public int ButtonMax { private set; get; }

    //スライド
    public int SlideCount { private set; get; }
    public int SlideMax { private set; get; } = 0;

    //１行あたりの数
    public readonly int ColumnCount = 7;

    AbstractSecondItemAct<T> itemAct;

    public ItemIterator(int innerMax, int buttonMax)
    {
        InnerMax = innerMax;
        ButtonMax = buttonMax;
        UIMax = InnerMax < ButtonMax ? InnerMax : ButtonMax;
        SlideMax = InnerMax < ButtonMax ? 0 : (InnerMax - ButtonMax) / ColumnCount + 1;
    }

    public void Up()
    {
        if(InnerNow - ColumnCount >= 0)
        {
            InnerNow -= ColumnCount;
            if(UINow - ColumnCount  >= 0)
            {
                UINow -= ColumnCount;
            }
            else
            {
                SlideCount--;
                //UIMaxの更新処理
                UIMax = InnerMax - SlideCount * ColumnCount > ButtonMax ? InnerMax - SlideCount * ColumnCount : ButtonMax;
            }
        }
    }

    public void Down()
    {
        if (InnerNow + ColumnCount < InnerMax)
        {
            InnerNow += ColumnCount;

            if (UINow + ColumnCount < UIMax)
            {
                //スライドしない
                UINow += ColumnCount;
            }
            else
            {
                //スライドする
                SlideCount++;
                UIMax = InnerMax - SlideCount * ColumnCount > ButtonMax ? InnerMax - SlideCount * ColumnCount : ButtonMax;
                itemAct.LayoutObjects();
            }
        }
        else if(SlideCount < SlideMax)
        {
            UINow -= ColumnCount;
            SlideCount++;
            UIMax = InnerMax - SlideCount * ColumnCount > ButtonMax ? InnerMax - SlideCount * ColumnCount : ButtonMax;
            itemAct.LayoutObjects();
        }
    }

    public void Left()
    {
        if (InnerNow > 0 && UINow % ColumnCount != 0)
        {
            InnerNow--;
            UINow--;
        }
    }

    public void Right()
    {
        if(InnerNow + 1 < InnerMax && UINow % ColumnCount != ColumnCount - 1)
        {
            InnerNow++;
            UINow++;
        }
    }
}

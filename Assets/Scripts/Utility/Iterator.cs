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
        for(int i = 0; i < maxFacilityAct.Length; i++)
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

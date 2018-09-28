using System.Collections;

public class DateData
{
    public int Year;
    public int Month;
    public int Week;
    public int Day;

    public void EndDay()
    {
        Day++;
        checkWeek();
        checkMonth();
        checkYear();
    }

    void checkWeek()
    {
        if (Day > 7)
        {
            Week++;
            Day = 1;
        }
    }

    void checkMonth()
    {
        if (Week > 4)
        {
            Month++;
            Week = 1;
        }
    }

    void checkYear()
    {
        if (Month > 12)
        {
            Year++;
            Month = 1;
        }
    }
}

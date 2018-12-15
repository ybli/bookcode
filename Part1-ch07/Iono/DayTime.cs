namespace Iono
{
    public class DayTime
    {
        enum TimeType
        {
            MJD,
            ANSITime,
            CivilTime,
            Epoch,
            SystemTime,
            UnixTime,
            YDSTime,
            JulianDate,
            GPSZcount,
            Week,
            GPSWeek,
            WeekSecond,
            BDSWeekSecond,
            GALWeekSecond,
            GPSWeekSecond,
            QZSWeekSecond
        }
         double _values;
        TimeSystem System { get; set; }

    }
}

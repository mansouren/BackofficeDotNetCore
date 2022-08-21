namespace Utilities;

public static class MethodExtentions
{
    public static long ToLong(this object o)
    {
        try
        {
            return Convert.ToInt64(o);
        }
        catch
        {
            return 0;
        }
    }

    public static int ToInt(this object o)
    {
        try
        {
            return Convert.ToInt32(o);
        }
        catch
        {
            return 0;
        }
    }

    public static long? ToNullableLong(this object o)
    {
        try
        {
            return Convert.ToInt64(o);
        }
        catch
        {
            return null;
        }
    }

    public static int? ToNullableInt(this object o)
    {
        try
        {
            return Convert.ToInt32(o);
        }
        catch
        {
            return null;
        }
    }

    public static DateTime ToRoundSecond(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
    }

    public static bool ToBoolean(this object b)
    {
        try
        {
            return Convert.ToBoolean(b);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static long ToTimeStamp(this DateTime dt)
    {
        var DateTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        var F = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return DateTime.Subtract(F).TotalMilliseconds.ToLong();
    }

}

namespace Remita.Services.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimeStamp(this DateTime date)
        {
            DateTime epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan t = (date.ToUniversalTime() - epochTime);
            return (long)t.TotalMilliseconds;
        }

        public static DateTime ToDateTime(this long milliseconds)
        {
            DateTime epochDateTime = new DateTime(1970, 1, 1);
            return epochDateTime.AddMilliseconds(milliseconds);
        }
    }
}

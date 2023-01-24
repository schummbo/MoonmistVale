public static class TimeUtilities
{
    public const float SecondsInDay = SecondsInHour * 24;
    public const float SecondsInHour = SecondsInMinute * 60;
    public const float SecondsInMinute = 60;
    public const float PhaseLengthSeconds = 900;

    public static int GetPhase(float currentTimeSeconds)
    {
        return (int)(currentTimeSeconds / PhaseLengthSeconds);
    }

    public static int GetPhaseByHour(int hour)
    {
        return GetPhase(hour * SecondsInHour);
    }
}

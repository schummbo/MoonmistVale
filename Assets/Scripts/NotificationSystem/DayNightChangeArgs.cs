namespace Assets.Scripts.NotificationSystem
{
    public class DayNightChangeArgs
    {
        public DayNightCycleState State { get; }

        public DayNightChangeArgs(DayNightCycleState state)
        {
            State = state;
        }
    }
}

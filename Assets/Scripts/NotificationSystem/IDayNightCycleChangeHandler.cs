namespace Assets.Scripts.NotificationSystem
{
    public interface IDayNightCycleChangeHandler
    {
        public const string ChangeHandlerName = nameof(HandleDayNightCycleChange);
        void HandleDayNightCycleChange(DayNightChangeArgs args);
    }
}

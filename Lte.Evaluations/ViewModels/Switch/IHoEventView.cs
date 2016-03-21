namespace Lte.Evaluations.ViewModels.Switch
{
    public interface IHoEventView
    {
        int Hysteresis { get; set; }
        int TimeToTrigger { get; set; }
    }
}
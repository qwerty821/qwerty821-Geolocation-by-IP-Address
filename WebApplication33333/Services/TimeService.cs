namespace WebApplication.Services
{
    public class TimeService : ITimeService
    {
        public string GetTime() => DateTime.Now.ToString();
    }
}

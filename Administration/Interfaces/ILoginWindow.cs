namespace Administration.Interfaces
{
    public interface ILoginWindow
    {
        void IndicateSuccess();
        string Password { get; set; }
    }
}
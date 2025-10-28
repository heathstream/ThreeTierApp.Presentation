namespace ThreeTierApp.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dataManager = new ThreeTierApp.Data.DataClass();

            dataManager.GimmeMoney();
        }
    }
}

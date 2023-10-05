namespace Ex03.ConsoleUI
{
    using Ex03.GarageLogic;

    internal class Program
    {
        public static void Main()
        {
            RunProgram();
        }

        public static void RunProgram()
        {
            GarageUiManager newGarageUiManager = new GarageUiManager();
            Garage newGarage = new Garage();
            newGarageUiManager.StartGarageUiManager(newGarage);
        }
    }
}

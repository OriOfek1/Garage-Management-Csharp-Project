namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private readonly eColor r_Color;
        private readonly eAmountOfDoors r_AmountOfDoors;
        public static readonly float sr_MaximalFuelTankCapacity = 52;
        public static readonly float sr_MaximalBatteryHours = 4.5f;
        public static readonly int sr_NumberOfTires = 4;
        public static readonly float sr_MaximalTireAirPressure = 27;

        public Car(
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_LicenseNumber,
            string i_Model,
            eColor i_Color,
            eAmountOfDoors i_AmountOfDoors)
            : base(i_OwnerName, i_OwnerPhoneNumber, i_LicenseNumber, i_Model)
        {
            r_Color = i_Color;
            r_AmountOfDoors = i_AmountOfDoors;
        }

        public override string ToString()
        {
            string carDetails = string.Format(
                @"{0}Car Color: {1}
Car Amount of doors: {2}",
                base.ToString(),
                r_Color,
                r_AmountOfDoors);

            return carDetails;
        }
    }
}
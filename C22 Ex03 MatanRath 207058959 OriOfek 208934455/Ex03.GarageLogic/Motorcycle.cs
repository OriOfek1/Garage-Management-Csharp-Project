namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineCapacity;
        public static readonly float sr_MaximalFuelTankCapacity = 5.4f;
        public static readonly float sr_MaximalBatteryHours = 2.8f;
        public static readonly int sr_NumberOfTires = 2;
        public static readonly float sr_MaximalTireAirPressure = 31;

        public Motorcycle(
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_LicenseNumber,
            string i_Model,
            eLicenseType i_LicenseType,
            int i_EngineCapacity)
            : base(i_OwnerName, i_OwnerPhoneNumber, i_LicenseNumber, i_Model)
        {
            r_LicenseType = i_LicenseType;
            r_EngineCapacity = i_EngineCapacity;
        }

        public override string ToString()
        {
            string motorcycleDetails = string.Format(
                @"{0}Motorcycle License Type: {1}
Motorcycle Engine Capacity: {2}",
                base.ToString(),
                r_LicenseType,
                r_EngineCapacity);

            return motorcycleDetails;
        }
    }
}

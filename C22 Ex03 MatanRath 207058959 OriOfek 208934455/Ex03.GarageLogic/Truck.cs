namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public static readonly int sr_NumberOfTires = 16;
        public static readonly float sr_MaximalTireAirPressure = 25;
        public static readonly float sr_MaximalFuelTankCapacity = 135;

        public bool IsTransferingRefrigiratedContent { get; set; }

        public float MaximalCargoWeight { get; set; }

        public Truck(
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_LicenseNumber,
            string i_Model,
            bool i_IsTransferingRefrigiratedContent,
            float i_MaximalCargoWeight)
            : base(i_OwnerName, i_OwnerPhoneNumber, i_LicenseNumber, i_Model)
        {
            IsTransferingRefrigiratedContent = i_IsTransferingRefrigiratedContent;
            MaximalCargoWeight = i_MaximalCargoWeight;
        }

        public override string ToString()
        {
            string truckDetails = string.Format(
                @"{0}Is transporting refrigerated content: {1}
Truck maximal cargo weight: {2}",
                base.ToString(),
                IsTransferingRefrigiratedContent,
                MaximalCargoWeight);

            return truckDetails;
        }
    }
}

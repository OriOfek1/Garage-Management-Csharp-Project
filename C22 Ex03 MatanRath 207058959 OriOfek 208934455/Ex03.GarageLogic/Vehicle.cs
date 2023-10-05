namespace Ex03.GarageLogic
{
    using System.Text;
    using System.Collections.Generic;

    public abstract class Vehicle
    {
        private string m_OwnerName;
        private string m_OwnerPhone;
        private string m_LicenseNumber;
        private string m_Model;
        private float m_RemainingEnergyLevelPercentage;
        public List<Tire> Tires;
        public Engine VehicleEngine = null;
        public eRepairStatus m_RepairStatus = eRepairStatus.InService;

        public string OwnerName
        {
            get => m_OwnerName;
        }

        public string OwnerPhone
        {
            get => m_OwnerPhone;
        }

        public string LicenseNumber
        {
            get => m_LicenseNumber;
        }

        public string Model
        {
            get => m_Model;
        }

        public eRepairStatus RepairStatus
        {
            get => m_RepairStatus;
            set => m_RepairStatus = value;
        }

        public float RemainingEnergyPercentage
        {
            get => m_RemainingEnergyLevelPercentage;
            set => m_RemainingEnergyLevelPercentage = value;
        }

        protected Vehicle(
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_LicenseNumber,
            string i_Model)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhoneNumber;
            m_LicenseNumber = i_LicenseNumber;
            m_Model = i_Model;
        }

        public override string ToString()
        {
            StringBuilder basicDetails = new StringBuilder();
            StringBuilder tiresStatus = new StringBuilder();
            int indexForTires = 1;

            string licenseNumber = string.Format(@"License Number: {0}", LicenseNumber);
            string ownerName = string.Format(@"Owner Name: {0}", OwnerName);
            string ownerPhone = string.Format(@"Owner Phone: {0}", OwnerPhone);
            string repairStatus = string.Format(@"Repair Status: {0}", RepairStatus);
            string modelName = string.Format(@"Model Name: {0}", Model);
            string engineDetails = string.Empty;

            tiresStatus.AppendLine("Tires Status:");
            foreach (Tire tire in Tires)
            {
                string tireStatus = string.Format(
                    @"  {0}) Manufacturer: {1}   Tire Pressure: {2} psi",
                    indexForTires,
                    tire.Manufacturer,
                    tire.CurrentAirPressure);
                indexForTires++;
                tiresStatus.AppendLine(tireStatus);
            }

            switch(VehicleEngine)
            {
                case FuelEngine currentFuelEngine:
                    engineDetails = string.Format(
                        @"Fuel Type: {0}
Fuel Percentage Remaining: {1}%",
                        currentFuelEngine.FuelType,
                        currentFuelEngine.RemainingEnergyPercentage);
                    break;
                case ElectricEngine currentElectricEngine:
                    engineDetails = string.Format(
                        @"Battery Percentage Remaining: {0}%",
                        currentElectricEngine.RemainingEnergyPercentage);
                    break;
            }

            basicDetails.AppendLine(licenseNumber);
            basicDetails.AppendLine(modelName);
            basicDetails.AppendLine(ownerName);
            basicDetails.AppendLine(ownerPhone);
            basicDetails.AppendLine(repairStatus);
            basicDetails.Append(tiresStatus.ToString());
            basicDetails.AppendLine(engineDetails);

            return basicDetails.ToString();
        }
    }
}
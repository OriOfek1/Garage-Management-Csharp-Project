namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Garage
    {
        public List<Vehicle> GarageVehicles;
        public int VehicleTypesCount = Enum.GetNames(typeof(eVehicleType)).Length;
        public int CarColorsCount = Enum.GetNames(typeof(eColor)).Length;
        public int CarAmountOfDoorsOptionsCount = Enum.GetNames(typeof(eAmountOfDoors)).Length;
        public int MotorcycleLicenseTypesCount = Enum.GetNames(typeof(eLicenseType)).Length;
        public int InRepairOptionsCount = Enum.GetNames(typeof(eRepairStatus)).Length;
        public int FuelTypesCount = Enum.GetNames(typeof(eFuelType)).Length;

        public Garage()
        {
            GarageVehicles = new List<Vehicle>();
        }

        public void InsertVehicleToGarage(BaseVehicleDetails i_BaseVehicleDetails, SpecificVehicleDetails i_SpecificVehicleDetails)
        {
            Vehicle newVehicle = VehicleCreator.CreateNewVehicle(i_BaseVehicleDetails, i_SpecificVehicleDetails);
            GarageVehicles.Add(newVehicle);
        }

        public Vehicle GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            if(!IsVehicleExistsInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("No vehicle with this license number exists in our garage!");
            }

            return GarageVehicles.Find(i_Vehicle => i_Vehicle.LicenseNumber == i_LicenseNumber);
        }

        public bool IsVehicleExistsInGarage(string i_LicenseNumber)
        {
            return GarageVehicles.Exists(i_Vehicle => i_Vehicle.LicenseNumber == i_LicenseNumber);
        }

        public void ChangeVehicleRepairStatusToInService(string i_LicenseNumber)
        {
            Vehicle currentVehicle = GetVehicleByLicenseNumber(i_LicenseNumber);
            currentVehicle.RepairStatus = eRepairStatus.InService;
        }

        public void ChangeVehicleRepairStatus(string i_LicenseNumber, eRepairStatus i_RepairStatus)
        {
            Vehicle selectedVehicle = GetVehicleByLicenseNumber(i_LicenseNumber);

            if (selectedVehicle.RepairStatus == i_RepairStatus)
            {
                string exceptionString = string.Format(
                    @"The selected vehicle is already have repair status: {0}",
                    i_RepairStatus);
                throw new ArgumentException(exceptionString);
            }

            selectedVehicle.RepairStatus = i_RepairStatus;
        }

        public List<Vehicle> GetVehiclesFromGarageByRepairStatus(eRepairStatus i_RepairStatus)
        {
            return GarageVehicles.Where(i_Vehicle => i_Vehicle.m_RepairStatus == i_RepairStatus).ToList();
        }

        public bool IsGarageEmpty()
        {
            return GarageVehicles.Count == 0;
        }

        public bool isNoVehiclesWithGivenRepairStatusInGarage(eRepairStatus i_RepairStatus)
        {
            return GetVehiclesFromGarageByRepairStatus(i_RepairStatus).Count == 0;
        }

        public void InflateVehicleTiresToMax(string i_LicenseNumber)
        {
            Vehicle currentVehicle = GetVehicleByLicenseNumber(i_LicenseNumber);
            foreach(Tire tire in currentVehicle.Tires)
            {
                tire.InflateToMax();
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            Vehicle currentVehicle = GetVehicleByLicenseNumber(i_LicenseNumber);

            if (currentVehicle.VehicleEngine is FuelEngine currentEngine)
            {
                currentEngine.Refuel(i_AmountOfFuelToAdd, i_FuelType);
                currentVehicle.RemainingEnergyPercentage = currentEngine.GetRemainingEnergyPercentage();
            }
            else
            {
                throw new ArgumentException("Chosen vehicle is not a fuel engine vehicle!");
            }
        }

        public void RechargeVehicle(string i_LicenseNumber, float i_AmountOfBatteryMinutesToAdd)
        {
            Vehicle currentVehicle = GetVehicleByLicenseNumber(i_LicenseNumber);

            if (currentVehicle.VehicleEngine is ElectricEngine currentEngine)
            {
                currentEngine.Recharge(i_AmountOfBatteryMinutesToAdd);
                currentVehicle.RemainingEnergyPercentage = currentEngine.GetRemainingEnergyPercentage();
            }
            else
            {
                throw new ArgumentException("Chosen vehicle is not an electric engine vehicle!");
            }
        }

        public string GetVehicleDetails(string i_LicenseNumber)
        {
            Vehicle currentVehicle = GetVehicleByLicenseNumber(i_LicenseNumber);
            return currentVehicle.ToString();
        }
    }
}
namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class VehicleCreator
    {
        public static Vehicle CreateNewVehicle(BaseVehicleDetails i_BaseVehicleDetails, SpecificVehicleDetails i_SpecificVehicleDetails)
        {
            Vehicle newVehicle = null;
            Engine newEngine = createNewEngine(i_BaseVehicleDetails.VehicleType, i_BaseVehicleDetails.CurrentRemainingEnergyInEngine);
            List<Tire> newVehicleTires = createNewTiresList(
                i_BaseVehicleDetails.VehicleType,
                i_BaseVehicleDetails.TiresManufacturer,
                i_BaseVehicleDetails.CurrentTireAirPressure);

            switch (i_BaseVehicleDetails.VehicleType)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    newVehicle = new Car(
                        i_BaseVehicleDetails.OwnerName,
                        i_BaseVehicleDetails.OwnerPhoneNumber,
                        i_BaseVehicleDetails.LicenseNumber,
                        i_BaseVehicleDetails.Model,
                        i_SpecificVehicleDetails.CarColor,
                        i_SpecificVehicleDetails.CarAmountOfDoors);
                    break;

                case eVehicleType.FuelMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new Motorcycle(
                        i_BaseVehicleDetails.OwnerName,
                        i_BaseVehicleDetails.OwnerPhoneNumber,
                        i_BaseVehicleDetails.LicenseNumber,
                        i_BaseVehicleDetails.Model,
                        i_SpecificVehicleDetails.MotorcycleLicenseType,
                        i_SpecificVehicleDetails.MotorcycleEngineCapacity);
                    break;

                case eVehicleType.Truck:
                    newVehicle = new Truck(
                        i_BaseVehicleDetails.OwnerName,
                        i_BaseVehicleDetails.OwnerPhoneNumber,
                        i_BaseVehicleDetails.LicenseNumber,
                        i_BaseVehicleDetails.Model,
                        i_SpecificVehicleDetails.TruckIsTransportingCoolCargo,
                        i_SpecificVehicleDetails.TruckMaximalCargoCapacity);
                    break;
            }

            newVehicle.VehicleEngine = newEngine;
            newVehicle.RemainingEnergyPercentage = newEngine.CurrentEnergyAmount / newEngine.MaximumEnergyAmount * 100;
            newVehicle.Tires = newVehicleTires;

            return newVehicle;
        }

        private static List<Tire> createNewTiresList(eVehicleType i_VehicleType, string i_TiresManufacturer, float i_CurrentTireAirPressure)
        {
            List<Tire> tires;
            int numberOfTires;
            float maximalTireAirPressure;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    tires = new List<Tire>(Car.sr_NumberOfTires);
                    maximalTireAirPressure = Car.sr_MaximalTireAirPressure;
                    numberOfTires = Car.sr_NumberOfTires;
                    break;

                case eVehicleType.FuelMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    tires = new List<Tire>(Motorcycle.sr_NumberOfTires);
                    maximalTireAirPressure = Motorcycle.sr_MaximalTireAirPressure;
                    numberOfTires = Motorcycle.sr_NumberOfTires;
                    break;

                case eVehicleType.Truck:
                    tires = new List<Tire>(Truck.sr_NumberOfTires);
                    maximalTireAirPressure = Truck.sr_MaximalTireAirPressure;
                    numberOfTires = Truck.sr_NumberOfTires;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(i_VehicleType), i_VehicleType, "Please choose an option within the valid range!");
            }

            for(int i = 0; i < numberOfTires; i++)
            {
                Tire tire = new Tire
                {
                    Manufacturer = i_TiresManufacturer,
                    CurrentAirPressure = i_CurrentTireAirPressure,
                    MaximalAirPressure = maximalTireAirPressure,
                };

                tires.Add(tire);
            }

            return tires;
        }

        private static Engine createNewEngine(eVehicleType i_VehicleType, float i_CurrentEnergyLevel)
        {
            Engine newEngine = null;
            eFuelType fuelType = 0;
            float maximumEngineEnergyCapacity = 0;

            switch(i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    fuelType = eFuelType.Octan95;
                    maximumEngineEnergyCapacity = Car.sr_MaximalFuelTankCapacity;
                    break;
                case eVehicleType.FuelMotorcycle:
                    fuelType = eFuelType.Octan98;
                    maximumEngineEnergyCapacity = Motorcycle.sr_MaximalFuelTankCapacity;
                    break;
                case eVehicleType.Truck:
                    fuelType = eFuelType.Soler;
                    maximumEngineEnergyCapacity = Truck.sr_MaximalFuelTankCapacity;
                    break;
                case eVehicleType.ElectricCar:
                    maximumEngineEnergyCapacity = Car.sr_MaximalBatteryHours;
                    break;
                case eVehicleType.ElectricMotorcycle:
                    maximumEngineEnergyCapacity = Motorcycle.sr_MaximalBatteryHours;
                    break;
                default:
                    break;
            }

            switch(i_VehicleType)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.FuelMotorcycle:
                case eVehicleType.Truck:
                    newEngine = new FuelEngine(fuelType, maximumEngineEnergyCapacity, i_CurrentEnergyLevel);
                    break;
                case eVehicleType.ElectricCar:
                case eVehicleType.ElectricMotorcycle:
                    newEngine = new ElectricEngine(maximumEngineEnergyCapacity, i_CurrentEnergyLevel);
                    break;
            }

            return newEngine;
        }
    }

    public class BaseVehicleDetails
    {
        public string OwnerName { get; set; }

        public string OwnerPhoneNumber { get; set; }

        public string LicenseNumber { get; set; }

        public string Model { get; set; }

        public string TiresManufacturer { get; set; }

        public float CurrentTireAirPressure { get; set; }

        public eVehicleType VehicleType { get; set; }

        public float CurrentRemainingEnergyInEngine { get; set; }
    }

    public class SpecificVehicleDetails
    {
        public eColor CarColor { get; set; }

        public eAmountOfDoors CarAmountOfDoors { get; set; }

        public eLicenseType MotorcycleLicenseType { get; set; }

        public int MotorcycleEngineCapacity { get; set; }

        public bool TruckIsTransportingCoolCargo { get; set; }

        public float TruckMaximalCargoCapacity { get; set; }
    }
}

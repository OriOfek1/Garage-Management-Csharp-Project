namespace Ex03.ConsoleUI
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Ex03.GarageLogic;

    internal class GarageUiManager
    {
        private Garage m_Garage;
        private bool m_IsProgramRunning = true;

        public void StartGarageUiManager(Garage i_Garage)
        {
            m_Garage = i_Garage;
            while (m_IsProgramRunning)
            {
                showOpeningScreen();
                showMainMenu();
                getUserInput(1, 8);
            }
        }

        private void showOpeningScreen()
        {
            string openingArt = string.Format(
                @"  ______       ______          ______ _______ 
 / _____)  /\ (_____ \   /\   / _____(_______)
| /  ___  /  \ _____) ) /  \ | /  ___ _____   
| | (___)/ /\ (_____ ( / /\ \| | (___|  ___)  
| \____/| |__| |    | | |__| | \____/| |_____ 
 \_____/|______|    |_|______|\_____/|_______)
                                              ");
            string openingMsg = string.Format(
                @"Hello and welcome to Matan Rath & Ori Ofek Garage!
");
            Console.WriteLine(openingArt);
            Console.WriteLine(openingMsg);
        }

        private void showMainMenu()
        {
            string chooseOptionMsg = string.Format(
                @"Please choose your desired action number, and press enter:
");
            Console.WriteLine(chooseOptionMsg);
            string options = string.Format(
                @"1 - Insert new vehicle
2 - Show existing vehicles license numbers
3 - Change vehicle status
4 - Tire inflation of specific vehicle to maximum
5 - Refuel a vehicle
6 - Recharge a vehicle
7 - Show full details of a specific vehicle
8 - Exit");
            string requestInputLine = "Please enter your desired action: ";
            Console.WriteLine(options);
            Console.Write(requestInputLine);
        }

        private void getUserInput(int i_MinValue, int i_MaxValue)
        {
            bool isInputValid = false;
            while (isInputValid == false)
            {
                try
                {
                    int option = userSelectedOptionToInteger(i_MinValue, i_MaxValue);
                    isInputValid = true;
                    switch (option)
                    {
                        case 1:
                            Console.Clear();
                            insertNewVehicle();
                            break;
                        case 2:
                            Console.Clear();
                            showVehichlesInGarageByLicense(m_Garage.GarageVehicles);
                            break;
                        case 3:
                            Console.Clear();
                            changeVehicleStatus();
                            break;
                        case 4:
                            Console.Clear();
                            inflateTiresToMax();
                            break;
                        case 5:
                            Console.Clear();
                            refuelVehicle();
                            break;
                        case 6:
                            Console.Clear();
                            rechargeVehicle();
                            break;
                        case 7:
                            Console.Clear();
                            showVehicleDetails();
                            break;
                        case 8:
                            Console.WriteLine("Exiting...");
                            m_IsProgramRunning = false;
                            break;
                        default: break;
                    }
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
        }

        private int userSelectedOptionToInteger(int i_MinValue, int i_MaxValue)
        {
            string userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out int optionInteger))
            {
                throw new FormatException("Wrong format input. Input format is not an integer.");
            }

            if (!isUserInputOptionIsValid(optionInteger, i_MinValue, i_MaxValue))
            {
                throw new ValueOutOfRangeException(new Exception(), optionInteger, i_MinValue, i_MaxValue);
            }

            return optionInteger;
        }

        private bool isUserInputOptionIsValid(int i_UserInputOption, int i_MinValue, int i_MaxValue)
        {
            return i_UserInputOption >= i_MinValue && i_UserInputOption <= i_MaxValue;
        }

        private void insertNewVehicle()
        {
            BaseVehicleDetails basicVehicleDetails = new BaseVehicleDetails();
            SpecificVehicleDetails specificVehicleDetails = new SpecificVehicleDetails();

            bool isFinishedProvidingDetails = false;

            while (!isFinishedProvidingDetails)
            {
                try
                {
                    string newVehicleMsg = string.Format(
                        @"Thanks for choosing our garage. We'll try to give you the best service possible.
In order to Insert your vehicle we'll need you to provide details about the vehicle.
But first, please tell us your name: ");
                    Console.WriteLine(newVehicleMsg);

                    basicVehicleDetails.OwnerName = getUserName();
                    basicVehicleDetails.OwnerPhoneNumber = getUserPhoneNumber();

                    string afterProvidingNameAndPhoneMsg = string.Format(
                        @"Hi {0}! Let's continue with the process.
",
                        basicVehicleDetails.OwnerName);
                    Console.WriteLine(afterProvidingNameAndPhoneMsg);

                    basicVehicleDetails.LicenseNumber = getOwnerVehicleLicenseNumber();

                    basicVehicleDetails.VehicleType = getVehicleType();
                    eVehicleType ownerVehicleType = basicVehicleDetails.VehicleType;

                    basicVehicleDetails.Model = getUserVehicleModel();

                    basicVehicleDetails.TiresManufacturer = getTiresManufacturer();

                    switch (ownerVehicleType)
                    {
                        case eVehicleType.FuelCar:
                        case eVehicleType.ElectricCar:
                            basicVehicleDetails.CurrentTireAirPressure =
                                getCurrentAirPressure(Car.sr_MaximalTireAirPressure);
                            specificVehicleDetails.CarColor = getCarColor();
                            specificVehicleDetails.CarAmountOfDoors = getCarAmountOfDoors();
                            break;
                        case eVehicleType.FuelMotorcycle:
                        case eVehicleType.ElectricMotorcycle:
                            basicVehicleDetails.CurrentTireAirPressure =
                                getCurrentAirPressure(Motorcycle.sr_MaximalTireAirPressure);
                            specificVehicleDetails.MotorcycleLicenseType = getMotorcycleLicenseType();
                            specificVehicleDetails.MotorcycleEngineCapacity = getMotorcycleEngineCapacity();
                            break;
                        case eVehicleType.Truck:
                            basicVehicleDetails.CurrentTireAirPressure =
                                getCurrentAirPressure(Truck.sr_MaximalTireAirPressure);
                            specificVehicleDetails.TruckIsTransportingCoolCargo =
                                checkIfTruckTransportingRefrigeratedContent();
                            specificVehicleDetails.TruckMaximalCargoCapacity = getMaximalCargoWeight();
                            break;
                    }

                    switch (ownerVehicleType)
                    {
                        case eVehicleType.FuelCar:
                            basicVehicleDetails.CurrentRemainingEnergyInEngine = getCurrentFuelLevel(Car.sr_MaximalFuelTankCapacity);
                            break;
                        case eVehicleType.FuelMotorcycle:
                            basicVehicleDetails.CurrentRemainingEnergyInEngine = getCurrentFuelLevel(Motorcycle.sr_MaximalFuelTankCapacity);
                            break;
                        case eVehicleType.Truck:
                            basicVehicleDetails.CurrentRemainingEnergyInEngine = getCurrentFuelLevel(Truck.sr_MaximalFuelTankCapacity);
                            break;

                        case eVehicleType.ElectricCar:
                            basicVehicleDetails.CurrentRemainingEnergyInEngine = getCurrentBatteryHoursLeft(Car.sr_MaximalBatteryHours);
                            break;
                        case eVehicleType.ElectricMotorcycle:
                            basicVehicleDetails.CurrentRemainingEnergyInEngine = getCurrentBatteryHoursLeft(Motorcycle.sr_MaximalBatteryHours);
                            break;
                    }

                    isFinishedProvidingDetails = true;
                    m_Garage.InsertVehicleToGarage(basicVehicleDetails, specificVehicleDetails);
                    Console.WriteLine("Vehicle was successfully added to the garage!");
                    Console.WriteLine("Press any key to go back to the main menu...");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine(i_FormatException.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private float getCurrentAirPressure(float i_VehicleMaximalAirPressure)
        {
            bool isInputValid = false;
            float currentAirPressure = 0;

            do
            {
                try
                {
                    Console.Write("Please enter the current air pressure: ");
                    currentAirPressure = getCurrentAirPressureInput(i_VehicleMaximalAirPressure);
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return currentAirPressure;
        }

        private float getCurrentAirPressureInput(float i_VehicleMaximalAirPressure)
        {
            string currentAirPressureInput = Console.ReadLine();

            if (string.IsNullOrEmpty(currentAirPressureInput))
            {
                throw new NullReferenceException("Air pressure number can't be empty!");
            }

            if (!float.TryParse(currentAirPressureInput, out float currentAirPressure))
            {
                throw new FormatException("Air pressure must be a real number!");
            }

            if (!(currentAirPressure >= 1 && currentAirPressure <= i_VehicleMaximalAirPressure))
            {
                throw new ValueOutOfRangeException(
                    new Exception(),
                    currentAirPressure,
                    1,
                    i_VehicleMaximalAirPressure);
            }

            return currentAirPressure;
        }

        private string getUserName()
        {
            Console.Write("Please enter your name: ");
            return Console.ReadLine();
        }

        private string getUserPhoneNumber()
        {
            bool isInputValid = false;
            string ownerPhoneNumber = string.Empty;

            do
            {
                try
                {
                    Console.Write("Please enter your phone number: ");
                    ownerPhoneNumber = getOwnerPhoneNumberInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (isInputValid == false);

            return ownerPhoneNumber;
        }

        private string getOwnerPhoneNumberInput()
        {
            string ownerPhoneNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(ownerPhoneNumber))
            {
                throw new NullReferenceException("Phone number can't be empty!");
            }

            if (!isStringIsOnlyIntegers(ownerPhoneNumber))
            {
                throw new FormatException("Phone number can't contain non-digit characters!");
            }

            if (ownerPhoneNumber.Length != 10)
            {
                throw new FormatException("Phone number must have 10 digits.");
            }

            return ownerPhoneNumber;
        }

        private string getOwnerVehicleLicenseNumber()
        {
            bool isInputValid = false;
            string ownerLicenseNumber = string.Empty;

            do
            {
                try
                {
                    Console.Write("Please enter vehicle license number: ");
                    ownerLicenseNumber = getOwnerVehicleLicenseNumberInput();

                    if (checkIfLicenseNumberExists(ownerLicenseNumber))
                    {
                        Console.WriteLine("This license number already exists in the garage. Vehicle moved back to service.");
                        m_Garage.ChangeVehicleRepairStatusToInService(ownerLicenseNumber);
                        goBackToMainMenu();
                    }

                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return ownerLicenseNumber;
        }

        private string getOwnerVehicleLicenseNumberInput()
        {
            string ownerVehicleLicenseNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(ownerVehicleLicenseNumber))
            {
                throw new NullReferenceException("License number can't be empty!");
            }

            if (ownerVehicleLicenseNumber.Length != 7)
            {
                throw new FormatException("License number must have 7 characters.");
            }

            return ownerVehicleLicenseNumber;
        }

        private eVehicleType getVehicleType()
        {
            bool isInputValid = false;
            eVehicleType vehicleType = (eVehicleType)1;
            string getVehicleTypeMsg = string.Format(@"Please choose your vehicle type option number:
1 - FuelCar
2 - ElectricCar
3 - FuelMotorcycle
4 - ElectricMotorcycle
5 - Truck");
            Console.WriteLine(getVehicleTypeMsg);

            while (isInputValid == false)
            {
                try
                {
                    int vehicleTypeInputOption = getUserInputForVehicleType();
                    isInputValid = true;

                    switch (vehicleTypeInputOption)
                    {
                        case 1:
                            vehicleType = (eVehicleType)1;
                            break;
                        case 2:
                            vehicleType = (eVehicleType)2;
                            break;
                        case 3:
                            vehicleType = (eVehicleType)3;
                            break;
                        case 4:
                            vehicleType = (eVehicleType)4;
                            break;
                        case 5:
                            vehicleType = (eVehicleType)5;
                            break;
                    }
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- UnknownException error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }

            return vehicleType;
        }

        private int getUserInputForVehicleType()
        {
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                throw new NullReferenceException();
            }

            if (!int.TryParse(userInput, out int chosenOption))
            {
                throw new FormatException("Input must be an integer!");
            }

            if (!(chosenOption >= 1 && chosenOption <= m_Garage.VehicleTypesCount))
            {
                throw new ValueOutOfRangeException(new Exception(), chosenOption, 1, m_Garage.VehicleTypesCount);
            }

            return chosenOption;
        }

        private string getUserVehicleModel()
        {
            Console.Write("Please enter vehicle model: ");
            return Console.ReadLine();
        }

        private bool isStringIsOnlyIntegers(string i_StringToCheck)
        {
            bool v_IsOnlyIntegers = true;

            foreach (char ch in i_StringToCheck)
            {
                if (!char.IsDigit(ch))
                {
                    v_IsOnlyIntegers = false;
                    break;
                }
            }

            return v_IsOnlyIntegers;
        }

        private string getTiresManufacturer()
        {
            Console.Write("Please enter vehicle tires manufacturer: ");
            return Console.ReadLine();
        }

        private float getCurrentBatteryHoursLeft(float i_VehicleMaximalBatteryHours)
        {
            bool isInputValid = false;
            float currentBatteryHoursLeft = 0;

            do
            {
                try
                {
                    Console.Write("Please enter the current battery hours left: ");
                    currentBatteryHoursLeft = getCurrentBatteryHoursLeftInput(i_VehicleMaximalBatteryHours);
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return currentBatteryHoursLeft;
        }

        private float getCurrentBatteryHoursLeftInput(float i_VehicleMaximalBatteryHours)
        {
            string currentBatteryHoursLeftInput = Console.ReadLine();

            if (string.IsNullOrEmpty(currentBatteryHoursLeftInput))
            {
                throw new NullReferenceException("Fuel level input can't be empty!");
            }

            if (!float.TryParse(currentBatteryHoursLeftInput, out float currentBatteryHoursLeft))
            {
                throw new FormatException("Fuel level must be a real number!");
            }

            if (!(currentBatteryHoursLeft >= 0 && currentBatteryHoursLeft <= i_VehicleMaximalBatteryHours))
            {
                throw new ValueOutOfRangeException(
                    new Exception(),
                    currentBatteryHoursLeft,
                    0,
                    i_VehicleMaximalBatteryHours);
            }

            return currentBatteryHoursLeft;
        }

        private float getCurrentFuelLevel(float i_VehicleMaximalFuelTankCapacity)
        {
            bool isInputValid = false;
            float currentFuelLevel = 0;

            do
            {
                try
                {
                    Console.Write("Please enter the current fuel level: ");
                    currentFuelLevel = getCurrentFuelLevelInput(i_VehicleMaximalFuelTankCapacity);
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return currentFuelLevel;
        }

        private float getCurrentFuelLevelInput(float i_VehicleMaximalFuelTankCapacity)
        {
            string currentFuelLevelInput = Console.ReadLine();

            if (string.IsNullOrEmpty(currentFuelLevelInput))
            {
                throw new NullReferenceException("Fuel level input can't be empty!");
            }

            if (!float.TryParse(currentFuelLevelInput, out float currentFuelLevel))
            {
                throw new FormatException("Fuel level must be a real number!");
            }

            if (!(currentFuelLevel >= 0 && currentFuelLevel <= i_VehicleMaximalFuelTankCapacity))
            {
                throw new ValueOutOfRangeException(
                    new Exception(),
                    currentFuelLevel,
                    0,
                    i_VehicleMaximalFuelTankCapacity);
            }

            return currentFuelLevel;
        }

        private bool checkIfTruckTransportingRefrigeratedContent()
        {
            bool isInputValid = false;
            bool isTruckCanTransportRefrigContent = false;

            do
            {
                try
                {
                    Console.WriteLine("Does the truck can transport refrigerated content? Y/N");
                    isTruckCanTransportRefrigContent = getCheckIfTruckTransportingRefrigeratedContentInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return isTruckCanTransportRefrigContent;
        }

        private bool getCheckIfTruckTransportingRefrigeratedContentInput()
        {
            string isTruckCanTransportRefrigContentInput = Console.ReadLine();
            bool isTruckCanTransportRefrigContent = true;

            if (string.IsNullOrEmpty(isTruckCanTransportRefrigContentInput))
            {
                throw new NullReferenceException("Input can't be empty!");
            }

            if (isTruckCanTransportRefrigContentInput != "Y" && isTruckCanTransportRefrigContentInput != "y"
                                                            && isTruckCanTransportRefrigContentInput != "N"
                                                            && isTruckCanTransportRefrigContentInput != "n")
            {
                throw new FormatException("Input must be Y/N!");
            }

            if (isTruckCanTransportRefrigContentInput == "N" || isTruckCanTransportRefrigContentInput == "n")
            {
                isTruckCanTransportRefrigContent = false;
            }

            return isTruckCanTransportRefrigContent;
        }

        private float getMaximalCargoWeight()
        {
            bool isInputValid = false;
            float maximalCargoWeight = 0;

            do
            {
                try
                {
                    Console.Write("Please enter your truck maximal cargo weight: ");
                    maximalCargoWeight = getMaximalCargoWeightInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return maximalCargoWeight;
        }

        private int getMaximalCargoWeightInput()
        {
            string maximalCargoWeightInput = Console.ReadLine();

            if (string.IsNullOrEmpty(maximalCargoWeightInput))
            {
                throw new NullReferenceException("Maximal cargo weight can't be empty!");
            }

            if (!int.TryParse(maximalCargoWeightInput, out int motorcycleEngineCapacity))
            {
                throw new FormatException("Wrong format input. Input format needs to be only numbers.");
            }

            return motorcycleEngineCapacity;
        }

        private eLicenseType getMotorcycleLicenseType()
        {
            bool isInputValid = false;
            int selectedOption = 0;
            string getLicenseTypeMsg = string.Format(
                @"Please select you motorcycle license type:
1 - A
2 - AA
3 - B1
4 - BB");
            Console.WriteLine(getLicenseTypeMsg);

            do
            {
                try
                {
                    selectedOption = userSelectedOptionToInteger(1, m_Garage.MotorcycleLicenseTypesCount);
                    isInputValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return (eLicenseType)selectedOption;
        }

        private int getMotorcycleEngineCapacity()
        {
            bool isInputValid = false;
            int motorcycleEngineCapacity = 0;

            do
            {
                try
                {
                    Console.Write("Please enter your motorcycle engine capacity: ");
                    motorcycleEngineCapacity = getMotorcycleEngineCapacityInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return motorcycleEngineCapacity;
        }

        private int getMotorcycleEngineCapacityInput()
        {
            string motorcycleEngineCapacityInput = Console.ReadLine();

            if (string.IsNullOrEmpty(motorcycleEngineCapacityInput))
            {
                throw new NullReferenceException("Motorcycle engine capacity can't be empty!");
            }

            if (!int.TryParse(motorcycleEngineCapacityInput, out int motorcycleEngineCapacity))
            {
                throw new FormatException("Wrong format input. Input format needs to be only numbers.");
            }

            return motorcycleEngineCapacity;
        }

        private eAmountOfDoors getCarAmountOfDoors()
        {
            bool isInputValid = false;
            int selectedOption = 0;
            string getAmountOfDoorsMsg = string.Format(
                @"Please select your car number of doors:
2 - Two
3 - Three
4 - Four
5 - Five");
            Console.WriteLine(getAmountOfDoorsMsg);

            do
            {
                try
                {
                    selectedOption = userSelectedOptionToInteger(2, m_Garage.CarAmountOfDoorsOptionsCount + 1);
                    isInputValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return (eAmountOfDoors)selectedOption;
        }

        private eColor getCarColor()
        {
            bool isInputValid = false;
            int selectedOption = 0;
            string getColorMsg = string.Format(
                @"Please select you car color:
1 - White
2 - Gray
3 - Black
4 - Blue");
            Console.WriteLine(getColorMsg);
            do
            {
                try
                {
                    selectedOption = userSelectedOptionToInteger(1, m_Garage.CarColorsCount);
                    isInputValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return (eColor)selectedOption;
        }

        private void showVehichlesInGarageByLicense(List<Vehicle> i_Garage)
        {
            bool isFinished = false;
            bool isInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("===== Show current vehicles in garage =====");
                string optionsMsg = string.Format(
                    @"Choose the option to filter by:
1 - In Service     2 - Repaired     3 - Paid     4 - All");
                Console.WriteLine(optionsMsg);
                while (!isInputValid)
                {
                    try
                    {
                        int option = userSelectedOptionToInteger(1, 4);
                        isInputValid = true;
                        printVehicleInGarageByRepairStatus(i_Garage, option);
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        Console.Write("Please enter a valid number within the range: ");
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                    }
                }

                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
                isFinished = true;
            }
        }

        private void printVehicleInGarageByRepairStatus(List<Vehicle> i_Garage, int i_UserSelectedOption)
        {
            if (m_Garage.IsGarageEmpty())
            {
                Console.WriteLine("Garage is empty!");
            }
            else
            {
                if (i_UserSelectedOption == 4)
                {
                    Console.WriteLine("Vehicle License Number  |  Repair Status");

                    foreach (Vehicle vehicle in i_Garage)
                    {
                        string format = string.Format(
                            @"      {0}           |  {1}",
                            vehicle.LicenseNumber,
                            vehicle.RepairStatus);
                        Console.WriteLine(format);
                    }
                }
                else
                {
                    if (m_Garage.isNoVehiclesWithGivenRepairStatusInGarage((eRepairStatus)i_UserSelectedOption))
                    {
                        string noVehiclesMsg = string.Format(
                            @"No vehicles with repair status {0}!",
                            (eRepairStatus)i_UserSelectedOption);
                        Console.WriteLine(noVehiclesMsg);
                    }
                    else
                    {
                        List<Vehicle> vehiclesByRepairStatus =
                            m_Garage.GetVehiclesFromGarageByRepairStatus((eRepairStatus)i_UserSelectedOption);

                        Console.WriteLine("Vehicle License Number  |  Repair Status");

                        foreach (Vehicle vehicle in vehiclesByRepairStatus)
                        {
                            string format = string.Format(
                                @"      {0}           |  {1}",
                                vehicle.LicenseNumber,
                                vehicle.RepairStatus);
                            Console.WriteLine(format);
                        }
                    }
                }
            }
        }

        private void changeVehicleStatus()
        {
            bool isFinished = false;
            bool isVehicleLicenseInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("===== Change a vehicle's repair status =====");
                Console.Write("Please enter the desired vehicle license number: ");

                while (!isVehicleLicenseInputValid)
                {
                    try
                    {
                        string licenseNumber = getLicenseNumberForChangingStatusInput();
                        printCurrentVehicleRepairStatus(licenseNumber);
                        changeChosenVehicleRepairStatus(licenseNumber);
                        isVehicleLicenseInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void changeChosenVehicleRepairStatus(string i_LicenseNumber)
        {
            bool isFinished = false;
            bool isOptionInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("Please choose the new repair status for the vehicle:");
                StringBuilder optionsMsg = new StringBuilder();

                foreach (var status in Enum.GetValues(typeof(eRepairStatus)))
                {
                    string statusStr = string.Format(@"{0} - {1}     ", (int)status, status);
                    optionsMsg.Append(statusStr);
                }

                Console.WriteLine(optionsMsg.ToString());

                while (!isOptionInputValid)
                {
                    try
                    {
                        int option = userSelectedOptionToInteger(1, m_Garage.InRepairOptionsCount);
                        isOptionInputValid = true;
                        m_Garage.ChangeVehicleRepairStatus(i_LicenseNumber, (eRepairStatus)option);
                        string afterChangeMsg = string.Format(
                            @"Vehicle number {0} repair status has changed to {1}.",
                            i_LicenseNumber,
                            (eRepairStatus)option);
                        Console.WriteLine(afterChangeMsg);
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        Console.Write("Please enter a valid number within the range: ");
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                    }
                }

                isFinished = true;
            }
        }

        private void printCurrentVehicleRepairStatus(string i_LicenseNumber)
        {
            string statusMsg = string.Format(
                @"The current status of the vehicle with the license number of {0} is: {1}.",
                i_LicenseNumber,
                m_Garage.GetVehicleByLicenseNumber(i_LicenseNumber).RepairStatus);

            Console.WriteLine(statusMsg);
        }

        private string getLicenseNumberForChangingStatusInput()
        {
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                throw new NullReferenceException("License number can't be empty!");
            }

            if (userInput.Length != 7)
            {
                throw new FormatException("License number must have 7 characters.");
            }

            if (!m_Garage.IsVehicleExistsInGarage(userInput))
            {
                throw new ArgumentException("No vehicle with this license number exists in our garage!");
            }

            return userInput;
        }

        private string getUserInputForVehicleLicenseNumber()
        {
            string userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                throw new NullReferenceException("Vehicle license number can't be empty.");
            }

            if (!int.TryParse(userInput, out int licenseNumberInt))
            {
                throw new FormatException("Wrong format input. Input format needs to be only numbers.");
            }

            if (!m_Garage.IsVehicleExistsInGarage(userInput))
            {
                throw new ArgumentException("No vehicle with this license number exists in our garage!");
            }

            return userInput;
        }

        private void inflateTiresToMax()
        {
            bool isFinished = false;
            bool isInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("===== Inflate Vehicle Tires To Max =====");

                while (!isInputValid)
                {
                    try
                    {
                        Console.Write("Enter the desired vehicle to inflate tires to max: ");
                        string licenseNumberInput = getUserInputForVehicleLicenseNumber();
                        m_Garage.InflateVehicleTiresToMax(licenseNumberInput);
                        Console.WriteLine("All tires inflated to max air pressure!");
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void goBackToMainMenu()
        {
            Console.Write("Press any key to return to the main menu...");
            Console.ReadLine();
            Console.Clear();
            showMainMenu();
        }

        private bool checkIfLicenseNumberExists(string i_UserInputLicenseNumber)
        {
            return m_Garage.IsVehicleExistsInGarage(i_UserInputLicenseNumber);
        }

        private void refuelVehicle()
        {
            bool isFinished = false;
            bool isInputValid = false;
            const bool v_IsFuelEngine = true;

            while (!isFinished)
            {
                Console.WriteLine("===== Refuel Vehicle =====");

                while (!isInputValid)
                {
                    try
                    {
                        Console.WriteLine("Enter the desired vehicle license number to refuel: ");
                        string licenseNumberInput = getUserInputForVehicleLicenseNumber();
                        eFuelType fuelTypeToRefuel = getUserInputForFuelType();
                        float amountOfFuelToAdd = getUserInputForAmountOfEnergyToAdd(v_IsFuelEngine);
                        m_Garage.RefuelVehicle(licenseNumberInput, fuelTypeToRefuel, amountOfFuelToAdd);
                        Console.WriteLine("Vehicle with license {0} have been refueled!", licenseNumberInput);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private float getUserInputForAmountOfEnergyToAdd(bool i_IsFuelEngine)
        {
            bool isInputValid = false;
            bool isFinished = false;
            float amountOfEnergyToAdd = 0;

            while(!isFinished)
            {
                Console.WriteLine(i_IsFuelEngine ? "Choose the amount of fuel to add:" : "Choose the amount of battery hours to add:");

                while(!isInputValid)
                {
                    try
                    {
                        amountOfEnergyToAdd = getAmountOfEnergyToAddInput(i_IsFuelEngine);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                }

                isFinished = true;
            }

            return amountOfEnergyToAdd;
        }

        private float getAmountOfEnergyToAddInput(bool i_IsFuelEngine)
        {
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                string nulRefMsg =
                    i_IsFuelEngine ? "Amount of fuel can't be empty!" : "Amount of hours can't be empty!";
                throw new NullReferenceException(nulRefMsg);
            }

            if(!float.TryParse(userInput, out float amountOfEnergy))
            {
                string formatExMsg =
                    i_IsFuelEngine ? "Amount of fuel must be a real number!" : "Amount of hours must be a real number!";
                throw new FormatException(formatExMsg);
            }

            return amountOfEnergy;
        }

        private eFuelType getUserInputForFuelType()
        {
            bool isInputValid = false;
            bool isFinished = false;
            int option = 0;

            while (!isFinished)
            {
                Console.WriteLine("Choose the fuel type for the vehicle:");

                StringBuilder optionsMsg = new StringBuilder();

                foreach (var fuelType in Enum.GetValues(typeof(eFuelType)))
                {
                    string statusStr = string.Format(@"{0} - {1}     ", (int)fuelType, fuelType);
                    optionsMsg.Append(statusStr);
                }

                Console.WriteLine(optionsMsg.ToString());

                while (!isInputValid)
                {
                    try
                    {
                        option = userSelectedOptionToInteger(1, m_Garage.FuelTypesCount);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        Console.Write("Please enter a valid number within the range: ");
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                    }
                }

                isFinished = true;
            }

            return (eFuelType)option;
        }

        private void rechargeVehicle()
        {
            bool isFinished = false;
            bool isInputValid = false;
            const bool v_IsFuelEngine = true;

            while (!isFinished)
            {
                Console.WriteLine("===== Recharge Vehicle =====");

                while (!isInputValid)
                {
                    try
                    {
                        Console.WriteLine("Enter the desired vehicle license number to recharge: ");
                        string licenseNumberInput = getUserInputForVehicleLicenseNumber();
                        float amountOfBatteryHoursToAdd = getUserInputForAmountOfEnergyToAdd(!v_IsFuelEngine);
                        m_Garage.RechargeVehicle(licenseNumberInput, amountOfBatteryHoursToAdd);
                        Console.WriteLine("Vehicle with license {0} have been recharged!", licenseNumberInput);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void showVehicleDetails()
        {
            bool isFinished = false;
            bool isInputValid = false;

            while(!isFinished)
            {
                Console.WriteLine("===== Show Vehicle Details =====");

                while(!isInputValid)
                {
                    try
                    {
                        Console.Write("Enter the desired vehicle to show details: ");
                        string licenseNumberInput = getUserInputForVehicleLicenseNumber();
                        Console.WriteLine(m_Garage.GetVehicleDetails(licenseNumberInput));
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
namespace Ex03.GarageLogic
{
    using System;

    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;
        private readonly float r_MaximalFuelTankSize;
        private float m_CurrentFuelLevel;
        private float m_RemainingEnergyPercentage;

        public float RemainingEnergyPercentage
        {
            get => m_RemainingEnergyPercentage;
            set => m_RemainingEnergyPercentage = value;
        }

        public eFuelType FuelType
        {
            get => r_FuelType;
        }

        public FuelEngine(eFuelType i_FuelType, float i_MaximalFuelTankSize, float i_CurrentFuelLevel)
            : base(i_MaximalFuelTankSize, i_CurrentFuelLevel)
        {
            r_FuelType = i_FuelType;
            r_MaximalFuelTankSize = i_MaximalFuelTankSize;
            m_CurrentFuelLevel = i_CurrentFuelLevel;
            RemainingEnergyPercentage = m_CurrentFuelLevel / r_MaximalFuelTankSize * 100;
        }

        public void Refuel(float i_AmountOfFuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("This vehicle does not support this type of fuel.");
            }

            if (m_CurrentFuelLevel + i_AmountOfFuelToAdd > r_MaximalFuelTankSize)
            {
                throw new ValueOutOfRangeException(
                    new Exception(),
                    i_AmountOfFuelToAdd,
                    0,
                    r_MaximalFuelTankSize - m_CurrentFuelLevel);
            }

            m_CurrentFuelLevel += i_AmountOfFuelToAdd;
            RemainingEnergyPercentage = m_CurrentFuelLevel / r_MaximalFuelTankSize * 100;
        }

        public override float GetRemainingEnergyPercentage()
        {
            return RemainingEnergyPercentage;
        }
    }
}

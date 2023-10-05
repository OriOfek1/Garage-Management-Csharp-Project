namespace Ex03.GarageLogic
{
    using System;

    public class ElectricEngine : Engine
    {
        private readonly float r_MaximalBatteryHours;
        private float m_CurrentBatteryHours;
        private float m_RemainingEnergyPercentage;

        public float RemainingEnergyPercentage
        {
            get => m_RemainingEnergyPercentage;
            set => m_RemainingEnergyPercentage = value;
        }

        public ElectricEngine(float i_MaximalBatteryHours, float i_CurrentBatteryHours)
            : base(i_MaximalBatteryHours, i_CurrentBatteryHours)
        {
            r_MaximalBatteryHours = i_MaximalBatteryHours;
            m_CurrentBatteryHours = i_CurrentBatteryHours;
            RemainingEnergyPercentage = m_CurrentBatteryHours / r_MaximalBatteryHours * 100;
        }

        public void Recharge(float i_MinutesToAddToBattery)
        {
            float hoursToAdd = i_MinutesToAddToBattery / 60;

            if (m_CurrentBatteryHours + hoursToAdd > r_MaximalBatteryHours)
            {
                throw new ValueOutOfRangeException(
                            new Exception(),
                            hoursToAdd,
                            0,
                            r_MaximalBatteryHours - m_CurrentBatteryHours);
            }

            m_CurrentBatteryHours += hoursToAdd;
            RemainingEnergyPercentage = m_CurrentBatteryHours / r_MaximalBatteryHours * 100;
        }

        public override float GetRemainingEnergyPercentage()
        {
            return RemainingEnergyPercentage;
        }
    }
}

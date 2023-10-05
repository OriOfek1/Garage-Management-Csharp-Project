using System.Dynamic;

namespace Ex03.GarageLogic
{
    using System;

    public class Tire
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaximalAirPressure;

        public string Manufacturer
        {
            get => m_Manufacturer;
            set => m_Manufacturer = value;
        }

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            set => m_CurrentAirPressure = value;
        }

        public float MaximalAirPressure
        {
            get => m_MaximalAirPressure;
            set => m_MaximalAirPressure = value;
        }

        public void InflateTire(float i_AirPressureToAdd)
        {
            if (CurrentAirPressure + i_AirPressureToAdd > MaximalAirPressure)
            {
                throw new ValueOutOfRangeException(
                    new Exception(),
                    i_AirPressureToAdd,
                    0,
                    MaximalAirPressure - CurrentAirPressure);
            }

            CurrentAirPressure += i_AirPressureToAdd;
        }

        public void InflateToMax()
        {
            CurrentAirPressure = MaximalAirPressure;
        }
    }
}
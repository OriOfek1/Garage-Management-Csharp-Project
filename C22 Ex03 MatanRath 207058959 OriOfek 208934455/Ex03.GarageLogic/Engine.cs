namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public float CurrentEnergyAmount { get; set; }

        public float MaximumEnergyAmount { get; set; }

        protected Engine(float i_MaximumEnergyAmount, float i_CurrentEnergyAmount)
        {
            MaximumEnergyAmount = i_MaximumEnergyAmount;
            CurrentEnergyAmount = i_CurrentEnergyAmount;
        }

        public virtual float GetRemainingEnergyPercentage()
        {
            return CurrentEnergyAmount / MaximumEnergyAmount * 100;
        }
    }
}

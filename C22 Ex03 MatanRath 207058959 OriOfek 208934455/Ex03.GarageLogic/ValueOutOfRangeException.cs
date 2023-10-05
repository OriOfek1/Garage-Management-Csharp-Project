using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
		{
            public float MaxValue { get; set; }

            public float MinValue { get; set; }

            public ValueOutOfRangeException(Exception i_InnerException, float i_UserInputValue, float i_MinValue, float i_MaxValue)
                : base(string.Format("An error has been occurred with the input {0}. The legal range of values to input is between {1} - {2}.", i_UserInputValue, i_MinValue, i_MaxValue))
            {
                MinValue = i_MinValue;
                MaxValue = i_MaxValue;
            }
		}
}
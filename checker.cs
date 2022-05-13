using System;

namespace Checker
{
  public class BatteryManagement: IBatteryManagement
  {
    public bool IsBatteryOk(float temperature, float soc, float chargeRate)
    {
      return IsTemperatureInRange(temperature) && IsSocInRange(soc) && IsChargeRateInRange(chargeRate);
    }

    public bool IsTemperatureInRange(float temperature)
    {
      if (IsParameterUnderLimit(45,0,temperature) == false)
      {
        PrintBatteryStatus("Temperatue is out of range!");
        return false;
      }

      return true;
    }

    public bool IsSocInRange(float stateOfCharge)
    {
      if (IsParameterUnderLimit(80, 20, stateOfCharge) == false)
      {
        PrintBatteryStatus("State of Charge is out of range!");
        return false;
      }

      return true;
    }
    
    private static void PrintBatteryStatus(string statusMessage)
    {
      Console.WriteLine(statusMessage);
    }

    private bool IsParameterUnderLimit(int maxValue, int minValue, float actualValue)
    {
      if (actualValue > maxValue || actualValue <  minValue)
      {
        return false;
      }

      return true;
    }

    public bool IsChargeRateInRange(float chargeRate)
    {
      if (chargeRate > 0.8f)
      {
        PrintBatteryStatus("Charge Rate is out of range!");
        return false;
      }

      return true;
    }
  }
}

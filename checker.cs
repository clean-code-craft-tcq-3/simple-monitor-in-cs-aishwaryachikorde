using System;

namespace Checker
{
  public class BatteryManagement: IBatteryManagement
  {
    public bool IsBatteryOk(float temperature, float soc, float chargeRate)
    {
      return IsTemperatureInRange(temperature) && IsSocInRange(soc) && IsChangeRateInRange(chargeRate);
    }

    private bool IsTemperatureInRange(float temperature)
    {
      if (temperature < 0 || temperature > 45)
      {
        PrintBatteryStatus("Temperatue is out of range!");
        return false;
      }

      return true;
    }

    private bool IsSocInRange(float stateOfCharge)
    {
      if (stateOfCharge < 20 || stateOfCharge > 80)
      {
        PrintBatteryStatus("State of Charge is out of range!");
        return false;
      }

      return true;
    }

    private bool IsChangeRateInRange(float chargeRate)
    {
      if (chargeRate > 0.8)
      {
        PrintBatteryStatus("Charge Rate is out of range!");
        return false;
      }

      return true;
    }

    private static void PrintBatteryStatus(string statusMessage)
    {
      Console.WriteLine(statusMessage);
    }
  }
}

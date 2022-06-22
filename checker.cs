using System;
using System.Diagnostics;

namespace checker
{
  public class BatteryManagement: IBatteryManagement
  {
    public bool IsBatteryOk(float temperature, float soc, float chargeRate, char temperatureUnit)
    {
      return IsTemperatureInRange(temperature, temperatureUnit) && IsSocInRange(soc) && IsChargeRateInRange(chargeRate);
    }

    public bool IsTemperatureInRange(float temperature, char temperatureUnit)
    {
      if (temperatureUnit == 'F')
      {
        ConvertTemperatureToCelsius(temperature);
      }

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

    private bool IsParameterUnderLimit(float maxValue, float minValue, float actualValue)
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

    //Extended Operations
    public bool IsValueInWarningRange(int maxValue, int minValue, float actualValue)
    {
      float warningTolerance = maxValue * 0.05f;
      float lowerLimit = minValue + warningTolerance;
      float upperLimit = maxValue - warningTolerance;

      if (IsParameterUnderLimit(upperLimit, lowerLimit, actualValue) == false)
      {
        PrintBatteryStatus("Warning!!!! Battery levels exceeding the threshold.");
        return false;
      }

      return true;
    }

    public bool IsChargeRateInWarningRange(float maxValue, float actualValue)
    {
      float warningTolerance = maxValue * 0.05f;
      float upperLimit = maxValue - warningTolerance;

      if (actualValue > upperLimit)
      {
        PrintBatteryStatus("Warning!!!! Charge Rate exceeding the threshold");
        return false;
      }
      else
      {
        return true;
      }
    }

    private void ConvertTemperatureToCelsius(float temperature)
    {
      temperature = (temperature - 32) * (5 / 9);
    }
  }

  public interface IBatteryManagement
  {
    bool IsBatteryOk(float temperature, float stateOfCharge, float chargeRate, char temperatureUnit);

    bool IsTemperatureInRange(float temperature, char temperatureUnit);

    bool IsSocInRange(float stateOfCharge);

    bool IsChargeRateInRange(float chargeRate);

    bool IsValueInWarningRange(int maxValue, int minValue, float actualValue);

    bool IsChargeRateInWarningRange(float maxValue, float actualValue);
  }

  public class BatterySystemUnitTest
  {
    public static int Main()
    {
      CheckBatteryStatus();
      Console.WriteLine("All ok");
      return 0;
    }

    private static void CheckBatteryStatus()
    {
      IBatteryManagement testBatterySpecification = new BatteryManagement();

      //Check if temperature is in range
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(0, 'C'));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(113, 'F'));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(18, 'C'));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(68, 'F'));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(44, 'C'));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(111.2f, 'F'));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(-3, 'C') == false);
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(46, 'C') == false);
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(55, 'C') == false);
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(140, 'F') == false);
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(167, 'F') == false);

      //Check if State of charge is in range
      Debug.Assert(testBatterySpecification.IsSocInRange(20));
      Debug.Assert(testBatterySpecification.IsSocInRange(80));
      Debug.Assert(testBatterySpecification.IsSocInRange(30));
      Debug.Assert(testBatterySpecification.IsSocInRange(-20) == false);
      Debug.Assert(testBatterySpecification.IsSocInRange(81) == false);
      Debug.Assert(testBatterySpecification.IsSocInRange(19) == false);

      ////Check if Charge Rate is in range
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0));
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0.8f));
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0.6f));
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0.9f) == false);
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(1.22f) == false);
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(2.33f) == false);


      //Test the overall battery
      Debug.Assert(testBatterySpecification.IsBatteryOk(0, 20, 0.0f, 'C'));
      Debug.Assert(testBatterySpecification.IsBatteryOk(113, 80, 0.8f, 'F'));
      Debug.Assert(testBatterySpecification.IsBatteryOk(3, 30, 0.6f, 'C'));
      Debug.Assert(testBatterySpecification.IsBatteryOk(-1, 70, 0.9f, 'C') == false);
      Debug.Assert(testBatterySpecification.IsBatteryOk(46, 81, 0.79f, 'C') == false);
      Debug.Assert(testBatterySpecification.IsBatteryOk(122, 81, 0.79f, 'F') == false);


      //Extended Tests
      //Temperature
      Debug.Assert(testBatterySpecification.IsValueInWarningRange(45, 0, 2.2f));
      Debug.Assert(testBatterySpecification.IsValueInWarningRange(45, 0, 130f) == false);
      Debug.Assert(testBatterySpecification.IsValueInWarningRange(45, 0, 43f));

      // State of Charge
      Debug.Assert(testBatterySpecification.IsValueInWarningRange(80, 20, 77));
      Debug.Assert(testBatterySpecification.IsValueInWarningRange(80, 20, 45));
      Debug.Assert(testBatterySpecification.IsValueInWarningRange(80, 20, 23));

      // Charge Rate
      Debug.Assert(testBatterySpecification.IsChargeRateInWarningRange(0.8f, 0.4f));
      Debug.Assert(testBatterySpecification.IsChargeRateInWarningRange(0.8f, 0.77f) == false); 
    }
  }
 }

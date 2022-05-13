using System;
using System.Diagnostics;

namespace Checker
{
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
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(0));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(45));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(18));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(20));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(44));
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(-3) == false);
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(46) == false);
      Debug.Assert(testBatterySpecification.IsTemperatureInRange(55) == false);

      //Check if State of charge is in range
      Debug.Assert(testBatterySpecification.IsSocInRange(20));
      Debug.Assert(testBatterySpecification.IsSocInRange(80));
      Debug.Assert(testBatterySpecification.IsSocInRange(30));
      Debug.Assert(testBatterySpecification.IsSocInRange(-20) == false);
      Debug.Assert(testBatterySpecification.IsSocInRange(81) == false);
      Debug.Assert(testBatterySpecification.IsSocInRange(19) == false);

      ////Check if State of charge is in range
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0));
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0.8f));
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0.6f));
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(0.9f) == false);
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(1.22f) == false);
      Debug.Assert(testBatterySpecification.IsChargeRateInRange(2.33f) == false);

      Debug.Assert(testBatterySpecification.IsBatteryOk(0, 20, 0.0f));
      Debug.Assert(testBatterySpecification.IsBatteryOk(45, 80, 0.8f));
      Debug.Assert(testBatterySpecification.IsBatteryOk(3, 30, 0.6f));
      Debug.Assert(testBatterySpecification.IsBatteryOk(-1, 70, 0.9f) == false);
      Debug.Assert(testBatterySpecification.IsBatteryOk(46, 81, 0.79f) == false);
    }
  }
}

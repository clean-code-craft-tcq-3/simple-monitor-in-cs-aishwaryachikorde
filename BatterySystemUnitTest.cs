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
      Debug.Assert(testBatterySpecification.IsBatteryOk(0, 15, 3) == false);
      Debug.Assert(testBatterySpecification.IsBatteryOk(1, 21, 1) == false);
      Debug.Assert(testBatterySpecification.IsBatteryOk(-1, 79, 0.7f) == false);
      Debug.Assert(testBatterySpecification.IsBatteryOk(2, 24, 0.6f));
      Debug.Assert(testBatterySpecification.IsBatteryOk(44, 79, 0.7f));
    }
   }
}
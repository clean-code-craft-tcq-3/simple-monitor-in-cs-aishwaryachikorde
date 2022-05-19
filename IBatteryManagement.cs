namespace checker
{
  public interface IBatteryManagement
  {
     bool IsBatteryOk(float temperature, float stateOfCharge, float chargeRate);

     bool IsTemperatureInRange(float temperature);

     bool IsSocInRange(float stateOfCharge);

     bool IsChargeRateInRange(float chargeRate);
  }
}

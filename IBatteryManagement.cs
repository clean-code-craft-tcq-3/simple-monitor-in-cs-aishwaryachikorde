namespace Checker
{
  public interface IBatteryManagement
  {
     bool IsBatteryOk(float temperature, float stateOfCharge, float chargeRate);
  }
}

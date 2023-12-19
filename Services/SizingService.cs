using SolarApp.Entities;

namespace SolarApp.Services;

public class SizingService
{
    public decimal NumberModules(decimal billConsumption, decimal modulesPower)
    {
        return Math.Round(billConsumption * 12 / (0.75m/*eficiencia*/ * 1919.99m/*irradiacao anual media*/) * 1000 / modulesPower, 0, MidpointRounding.AwayFromZero);
    }

    public decimal SystemPower(decimal numberModules, decimal modulesPower)
    {
        return Math.Round(numberModules * modulesPower / 1000, 2);
    }

    public decimal SizingConsumption(IEnumerable<Bill> bills, decimal modulesPower)
    {
        return bills.Select(bill =>
        {
            var numberModules = NumberModules(bill.Consumption, modulesPower);
            var systemPower = SystemPower(numberModules, modulesPower);
            return systemPower * 0.75m * 1919.99m;
        }).Sum();
    }

    public decimal SizingAmount(IEnumerable<Bill> bills, decimal modulesPower)
    {
        return bills.Select(bill =>
        {
            var numberModules = NumberModules(bill.Amount / bill.Tariff, modulesPower);
            var systemPower = SystemPower(numberModules, modulesPower);
            return systemPower * 0.75m * 1919.99m;
        }).Sum();
    }

}


using SolarApp.Entities;

namespace SolarApp.Services;

public class SizingService
{
    private readonly IrradiationService _irradiationService;

    public SizingService(IrradiationService irradiationService)
    {
        _irradiationService = irradiationService;
    }

    public decimal IrradiationAnnualAverageCalculator(Project project)
    {
        var irradiation = _irradiationService.GetCityIrradiation(project);

        return Math.Round(
            (irradiation.Result!.Jan / 1000 * 31) +
            (irradiation.Result!.Feb / 1000 * 28) +
            (irradiation.Result!.Mar / 1000 * 31) +
            (irradiation.Result!.Apr / 1000 * 30) +
            (irradiation.Result!.May / 1000 * 31) +
            (irradiation.Result!.Jun / 1000 * 30) +
            (irradiation.Result!.Jul / 1000 * 31) +
            (irradiation.Result!.Aug / 1000 * 30) +
            (irradiation.Result!.Sep / 1000 * 30) +
            (irradiation.Result!.Oct / 1000 * 31) +
            (irradiation.Result!.Nov / 1000 * 30) +
            (irradiation.Result!.Dec / 1000 * 31),
            2
        );
    }


    public decimal NumberModules(decimal billConsumption, decimal modulesPower, decimal irradiationAverage)
    {
        return Math.Round(billConsumption * 12 / (0.75m/*eficiencia*/ * irradiationAverage) * 1000 / modulesPower, 0, MidpointRounding.AwayFromZero);
    }

    public decimal SystemPower(decimal numberModules, decimal modulesPower)
    {
        return Math.Round(numberModules * modulesPower / 1000, 2);
    }

    public decimal SizingByConsumption(IEnumerable<Bill> bills, decimal modulesPower, decimal irradiationAverage)
    {
        return bills.Select(bill =>
        {
            var numberModules = NumberModules(bill.Consumption, modulesPower, irradiationAverage);
            var systemPower = SystemPower(numberModules, modulesPower);
            return systemPower * 0.75m * irradiationAverage;
        }).Sum();
    }

    public decimal SizingByAmount(IEnumerable<Bill> bills, decimal modulesPower, decimal irradiationAverage)
    {
        return bills.Select(bill =>
        {
            var numberModules = NumberModules(bill.Amount / bill.Tariff, modulesPower, irradiationAverage);
            var systemPower = SystemPower(numberModules, modulesPower);
            return systemPower * 0.75m * irradiationAverage;
        }).Sum();
    }

}


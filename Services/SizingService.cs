using System.Text;
using SolarApp.Entities;

namespace SolarApp.Services;

public class SizingService
{
    private readonly IrradiationService _irradiationService;

    public SizingService(IrradiationService irradiationService)
    {
        _irradiationService = irradiationService;
    }

    public decimal CalculateGeneration(Project project, decimal irradiation, int days)
    {
        return Math.Round(project.Power * 0.75m * irradiation / 1000 * days, 2);

    }

    public decimal CalculateAnnualGenerationAverage(Project project)
    {
        var irradiation = _irradiationService.GetCityIrradiation(project).Result;

        return Math.Round(CalculateGeneration(project, irradiation!.Annual, 365) / 12, 2);
    }

    public MonthGeneration CalculateMonthGeneration(Project project)
    {
        var irradiation = _irradiationService.GetCityIrradiation(project).Result;
        return new MonthGeneration
        {
            Jan = CalculateGeneration(project, irradiation!.Jan, 31),
            Feb = CalculateGeneration(project, irradiation!.Feb, 28),
            Mar = CalculateGeneration(project, irradiation!.Mar, 31),
            Apr = CalculateGeneration(project, irradiation!.Apr, 30),
            May = CalculateGeneration(project, irradiation!.May, 31),
            Jun = CalculateGeneration(project, irradiation!.Jun, 30),
            Jul = CalculateGeneration(project, irradiation!.Jul, 31),
            Aug = CalculateGeneration(project, irradiation!.Aug, 31),
            Sep = CalculateGeneration(project, irradiation!.Sep, 30),
            Oct = CalculateGeneration(project, irradiation!.Oct, 31),
            Nov = CalculateGeneration(project, irradiation!.Nov, 30),
            Dec = CalculateGeneration(project, irradiation!.Dec, 31),
        };
    }

    public decimal CalculateBillsConsumption(IEnumerable<Bill> bills)
    {
        if (bills is not null)
        {
            var billsConsumption = bills.Where(bill => bill.Consumption != 0).Sum(bill => bill.Consumption);
            var calculatedConsumption = bills.Where(bill => bill.Consumption == 0).Sum(bill => bill.Amount / bill.Tariff);

            return Math.Round(billsConsumption + calculatedConsumption, 2);
        }
        return 0;
    }

    public decimal NumberModules(decimal billConsumption, decimal modulesPower, decimal annualIrradiationAverage)
    {
        return Math.Round(billConsumption * 12 / (0.75m/*eficiencia*/ * annualIrradiationAverage / 1000 * 365) * 1000 / modulesPower, 0, MidpointRounding.ToPositiveInfinity);
    }

    public decimal SystemPower(decimal numberModules, decimal modulesPower)
    {
        return Math.Round(numberModules * modulesPower / 1000, 2);
    }
}


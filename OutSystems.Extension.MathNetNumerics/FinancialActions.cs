namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Implements financial calculations including time value of money, depreciation, and amortization.
/// </summary>
public class FinancialActions
{
    /// <inheritdoc />
    public double FutureValue(double presentValue, double annualRate, int periods)
    {
        MathHelper.ValidateFinite(presentValue, nameof(presentValue));
        MathHelper.ValidateFinite(annualRate, nameof(annualRate));
        MathHelper.ValidateNonNegative(periods, nameof(periods));
        return presentValue * Math.Pow(1 + annualRate, periods);
    }

    /// <inheritdoc />
    public double PresentValue(double futureValue, double annualRate, int periods)
    {
        MathHelper.ValidateFinite(futureValue, nameof(futureValue));
        MathHelper.ValidateFinite(annualRate, nameof(annualRate));
        MathHelper.ValidateNonNegative(periods, nameof(periods));
        double divisor = Math.Pow(1 + annualRate, periods);
        if (divisor == 0)
            throw new ArgumentException("Annual rate of -100% with positive periods produces a zero divisor.", nameof(annualRate));
        return futureValue / divisor;
    }

    /// <inheritdoc />
    public double NetPresentValue(double discountRate, List<double> cashFlows)
    {
        MathHelper.ValidateFinite(discountRate, nameof(discountRate));
        MathHelper.ValidateNotNullOrEmpty(cashFlows, nameof(cashFlows));
        double npv = 0;
        for (int t = 0; t < cashFlows.Count; t++)
            npv += cashFlows[t] / Math.Pow(1 + discountRate, t);
        return npv;
    }

    /// <inheritdoc />
    public double InternalRateOfReturn(List<double> cashFlows)
    {
        MathHelper.ValidateNotNullOrEmpty(cashFlows, nameof(cashFlows));
        if (cashFlows.Count < 2)
            throw new ArgumentException("At least two cash flows are required to compute IRR.", nameof(cashFlows));

        double rate = 0.1;
        bool converged = false;
        for (int i = 0; i < 1000; i++)
        {
            double npv = 0, dnpv = 0;
            for (int t = 0; t < cashFlows.Count; t++)
            {
                double factor = Math.Pow(1 + rate, t);
                npv += cashFlows[t] / factor;
                if (t > 0) dnpv -= t * cashFlows[t] / Math.Pow(1 + rate, t + 1);
            }
            if (Math.Abs(npv) < 1e-10)
            {
                converged = true;
                break;
            }
            if (Math.Abs(dnpv) < 1e-15) break;
            rate -= npv / dnpv;
            if (rate < -1) rate = -0.99;
        }

        if (!converged)
            throw new InvalidOperationException("IRR did not converge. The cash flow series may not have a real IRR.");

        return rate;
    }

    /// <inheritdoc />
    public double PaymentAmount(double principal, double annualRate, int totalPeriods)
    {
        MathHelper.ValidateFinite(principal, nameof(principal));
        MathHelper.ValidateFinite(annualRate, nameof(annualRate));
        MathHelper.ValidatePositive(totalPeriods, nameof(totalPeriods));
        double r = annualRate / 12;
        if (Math.Abs(r) < 1e-12) return principal / totalPeriods;
        return principal * r * Math.Pow(1 + r, totalPeriods) / (Math.Pow(1 + r, totalPeriods) - 1);
    }

    /// <inheritdoc />
    public double CompoundInterest(double principal, double annualRate, int compoundingsPerYear, int years)
    {
        MathHelper.ValidateFinite(principal, nameof(principal));
        MathHelper.ValidateFinite(annualRate, nameof(annualRate));
        MathHelper.ValidatePositive(compoundingsPerYear, nameof(compoundingsPerYear));
        MathHelper.ValidateNonNegative(years, nameof(years));
        double fv = principal * Math.Pow(1 + annualRate / compoundingsPerYear, compoundingsPerYear * years);
        return fv - principal;
    }

    /// <inheritdoc />
    public double StraightLineDepreciation(double cost, double salvageValue, int usefulLifeYears)
    {
        MathHelper.ValidateFinite(cost, nameof(cost));
        MathHelper.ValidateFinite(salvageValue, nameof(salvageValue));
        MathHelper.ValidatePositive(usefulLifeYears, nameof(usefulLifeYears));
        return (cost - salvageValue) / usefulLifeYears;
    }

    /// <inheritdoc />
    public List<AmortizationScheduleEntry> AmortizationSchedule(double principal, double annualRate, int totalPeriods)
    {
        MathHelper.ValidateFinite(principal, nameof(principal));
        MathHelper.ValidateFinite(annualRate, nameof(annualRate));
        MathHelper.ValidatePositive(totalPeriods, nameof(totalPeriods));
        if (totalPeriods > 12_000)
            throw new ArgumentOutOfRangeException(nameof(totalPeriods), totalPeriods, "totalPeriods must not exceed 12,000.");
        decimal monthlyRate = (decimal)annualRate / 12;
        decimal payment = (decimal)PaymentAmount(principal, annualRate, totalPeriods);
        decimal balance = (decimal)principal;

        var schedule = new List<AmortizationScheduleEntry>();
        for (int period = 1; period <= totalPeriods; period++)
        {
            decimal interest = Math.Round(balance * monthlyRate, 2);
            decimal principalPart = Math.Round(payment - interest, 2);

            if (period == totalPeriods)
            {
                principalPart = balance;
                payment = principalPart + interest;
            }

            balance -= principalPart;
            if (balance < 0) balance = 0;

            schedule.Add(new AmortizationScheduleEntry
            {
                Period = period,
                Payment = payment,
                PrincipalPortion = principalPart,
                InterestPortion = interest,
                RemainingBalance = balance
            });
        }

        return schedule;
    }
}

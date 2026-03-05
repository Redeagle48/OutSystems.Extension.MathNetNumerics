using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Financial calculations for time value of money, loan analysis, and depreciation.
/// Exposed as the <c>MathNet_Financial</c> module in OutSystems Developer Cloud (ODC).
/// All interest rates are expressed as decimals (e.g., 0.05 for 5%).
/// </summary>
[OSInterface(Name = "MathNet_Financial", Description = "Financial calculations: NPV, IRR, compound interest, amortization", IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IFinancial
{
    /// <summary>
    /// Calculates the future value of a present amount using compound growth.
    /// Formula: FV = presentValue * (1 + annualRate) ^ periods.
    /// </summary>
    /// <param name="presentValue">The current value of the investment or loan. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.05 for 5%). Must be a finite number.</param>
    /// <param name="periods">Number of compounding periods. Must be non-negative.</param>
    /// <returns>The future value in the same currency unit as <paramref name="presentValue"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Calculates future value given present value, annual rate as decimal (e.g. 0.05 for 5%), and number of periods")]
    double FutureValue(double presentValue, double annualRate, int periods);

    /// <summary>
    /// Calculates the present value of a future amount by discounting.
    /// Formula: PV = futureValue / (1 + annualRate) ^ periods.
    /// </summary>
    /// <param name="futureValue">The future amount to be discounted. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.05 for 5%). Must be a finite number. Cannot be -1 (100% loss).</param>
    /// <param name="periods">Number of discounting periods. Must be non-negative.</param>
    /// <returns>The present value in the same currency unit as <paramref name="futureValue"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    /// <exception cref="ArgumentException">Thrown when annualRate is -1 with positive periods (zero divisor).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Calculates present value given future value, annual rate as decimal (e.g. 0.05 for 5%), and number of periods")]
    double PresentValue(double futureValue, double annualRate, int periods);

    /// <summary>
    /// Computes the Net Present Value (NPV) of a series of cash flows at a given discount rate.
    /// Each cash flow is discounted by (1 + discountRate) ^ t, where t is the zero-based period index.
    /// </summary>
    /// <param name="discountRate">Discount rate as a decimal (e.g., 0.10 for 10%). Must be a finite number.</param>
    /// <param name="cashFlows">List of cash flows ordered by period. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>The Net Present Value in the same currency unit as the cash flows.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when discountRate is NaN or Infinity.</exception>
    /// <exception cref="ArgumentNullException">Thrown when cashFlows is null.</exception>
    /// <exception cref="ArgumentException">Thrown when cashFlows is empty.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Net Present Value of a series of cash flows at a given discount rate as decimal (e.g. 0.10 for 10%)")]
    double NetPresentValue(double discountRate, List<double> cashFlows);

    /// <summary>
    /// Computes the Internal Rate of Return (IRR) for a series of cash flows using Newton-Raphson iteration.
    /// Converges when |NPV| &lt; 1e-10 or after 1,000 iterations.
    /// The first cash flow is typically a negative investment.
    /// </summary>
    /// <param name="cashFlows">List of cash flows with at least 2 elements. First value is typically negative (investment). Maximum 10,000,000 elements.</param>
    /// <returns>The IRR as a decimal (e.g., 0.12 for 12%).</returns>
    /// <exception cref="ArgumentNullException">Thrown when cashFlows is null.</exception>
    /// <exception cref="ArgumentException">Thrown when cashFlows has fewer than 2 elements.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the algorithm does not converge (no real IRR exists).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Internal Rate of Return for a series of cash flows (first value is typically negative investment). Returns rate as decimal")]
    double InternalRateOfReturn(List<double> cashFlows);

    /// <summary>
    /// Calculates the fixed monthly payment for a fully amortizing loan.
    /// Uses the standard annuity formula. When the monthly rate is near zero (&lt; 1e-12), returns principal / totalPeriods.
    /// </summary>
    /// <param name="principal">Loan principal amount. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.06 for 6%). Must be a finite number.</param>
    /// <param name="totalPeriods">Total number of monthly payment periods. Must be positive.</param>
    /// <returns>The fixed monthly payment amount in the same currency unit as <paramref name="principal"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Monthly payment for a loan given principal, annual rate as decimal (e.g. 0.06 for 6%), and total number of monthly periods")]
    double PaymentAmount(double principal, double annualRate, int totalPeriods);

    /// <summary>
    /// Calculates the compound interest earned: FV - principal.
    /// Formula: principal * (1 + annualRate / compoundingsPerYear) ^ (compoundingsPerYear * years) - principal.
    /// </summary>
    /// <param name="principal">Initial investment amount. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.05 for 5%). Must be a finite number.</param>
    /// <param name="compoundingsPerYear">Number of times interest compounds per year (e.g., 12 for monthly). Must be positive.</param>
    /// <param name="years">Number of years. Must be non-negative.</param>
    /// <returns>The compound interest amount (FV - principal) in the same currency unit as <paramref name="principal"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Calculates compound interest amount (FV - PV) given annual rate as decimal (e.g. 0.05 for 5%)")]
    double CompoundInterest(double principal, double annualRate, int compoundingsPerYear, int years);

    /// <summary>
    /// Calculates annual depreciation using the straight-line method.
    /// Formula: (cost - salvageValue) / usefulLifeYears.
    /// </summary>
    /// <param name="cost">Original asset cost. Must be a finite number.</param>
    /// <param name="salvageValue">Estimated residual value at end of useful life. Must be a finite number.</param>
    /// <param name="usefulLifeYears">Useful life in years. Must be positive.</param>
    /// <returns>The annual depreciation amount.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Annual depreciation using straight-line method: (cost - salvageValue) / usefulLifeYears")]
    double StraightLineDepreciation(double cost, double salvageValue, int usefulLifeYears);

    /// <summary>
    /// Generates a complete loan amortization schedule with per-period breakdown of principal, interest, and remaining balance.
    /// The last period is adjusted to eliminate rounding drift, ensuring the balance reaches exactly zero.
    /// </summary>
    /// <param name="principal">Loan principal amount. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.06 for 6%). Must be a finite number.</param>
    /// <param name="totalPeriods">Total number of monthly payment periods. Must be between 1 and 12,000.</param>
    /// <returns>A list of <see cref="AmortizationScheduleEntry"/> with one entry per period.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range. Thrown when totalPeriods exceeds 12,000.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Returns amortization schedule for monthly payments given annual rate as decimal (e.g. 0.06 for 6%)")]
    List<AmortizationScheduleEntry> AmortizationSchedule(double principal, double annualRate, int totalPeriods);
}

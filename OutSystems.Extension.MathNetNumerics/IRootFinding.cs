using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Root finding and break-even analysis.
/// Exposed as the <c>MathNet_RootFinding</c> module in OutSystems Developer Cloud (ODC).
/// Polynomial coefficients use the convention: a0 + a1*x + a2*x^2 + ... (constant term at index 0).
/// </summary>
[OSInterface(Name = "MathNet_RootFinding", Description = "Find roots of equations (break-even, equilibrium points)", IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IRootFinding
{
    /// <summary>
    /// Finds a root of a polynomial within [lowerBound, upperBound] using the bisection method.
    /// The function values at the bounds must have opposite signs (sign change required).
    /// Polynomial is evaluated using Horner's method for numerical stability.
    /// </summary>
    /// <param name="coefficients">Polynomial coefficients [a0, a1, a2, ...] where polynomial = a0 + a1*x + a2*x^2 + .... Must not be null or empty.</param>
    /// <param name="lowerBound">Lower bound of the search interval. Must be a finite number less than upperBound.</param>
    /// <param name="upperBound">Upper bound of the search interval. Must be a finite number greater than lowerBound.</param>
    /// <returns>The x value where the polynomial equals zero (within tolerance).</returns>
    /// <exception cref="ArgumentException">Thrown when coefficients is empty or lowerBound &gt;= upperBound.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when bounds are NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Finds root of a polynomial in range [lowerBound, upperBound] using bisection. Coefficients are a0 + a1*x + a2*x^2 + ...")]
    double BisectionRoot(List<double> coefficients, double lowerBound, double upperBound);

    /// <summary>
    /// Finds a root of a polynomial within [lowerBound, upperBound] using Brent's method.
    /// Generally converges faster than bisection. Requires a sign change across the interval.
    /// </summary>
    /// <param name="coefficients">Polynomial coefficients [a0, a1, a2, ...]. Must not be null or empty.</param>
    /// <param name="lowerBound">Lower bound of the search interval. Must be a finite number less than upperBound.</param>
    /// <param name="upperBound">Upper bound of the search interval. Must be a finite number greater than lowerBound.</param>
    /// <returns>The x value where the polynomial equals zero (within tolerance).</returns>
    /// <exception cref="ArgumentException">Thrown when coefficients is empty or lowerBound &gt;= upperBound.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when bounds are NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Finds root of a polynomial in range [lowerBound, upperBound] using Brent's method")]
    double BrentRoot(List<double> coefficients, double lowerBound, double upperBound);

    /// <summary>
    /// Calculates the break-even quantity where total revenue equals total cost.
    /// Formula: fixedCost / (pricePerUnit - variableCostPerUnit).
    /// Requires that pricePerUnit &gt; variableCostPerUnit (positive margin).
    /// </summary>
    /// <param name="pricePerUnit">Revenue per unit sold. Must be a finite number.</param>
    /// <param name="fixedCost">Total fixed costs. Must be a non-negative finite number.</param>
    /// <param name="variableCostPerUnit">Variable cost per unit. Must be a finite number less than pricePerUnit.</param>
    /// <returns>The break-even quantity (number of units).</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when parameters are NaN/Infinity or fixedCost is negative.</exception>
    /// <exception cref="ArgumentException">Thrown when pricePerUnit &lt;= variableCostPerUnit (zero or negative margin).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Finds break-even quantity where Revenue = Cost. Given price per unit, fixed cost, and variable cost per unit")]
    double BreakEvenQuantity(double pricePerUnit, double fixedCost, double variableCostPerUnit);
}

using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Numerical integration and area calculations.
/// Exposed as the <c>MathNet_Integration</c> module in OutSystems Developer Cloud (ODC).
/// </summary>
[OSInterface(Name = "MathNet_Integration", Description = "Numerical integration and area calculations", IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IIntegration
{
    /// <summary>
    /// Computes the area under a curve from paired x,y data points using the trapezoidal rule.
    /// Sums the areas of trapezoids formed between consecutive data points.
    /// </summary>
    /// <param name="xValues">X coordinates of the data points. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Y coordinates of the data points. Must have the same length as <paramref name="xValues"/>.</param>
    /// <returns>The approximate area under the curve.</returns>
    /// <exception cref="ArgumentNullException">Thrown when xValues or yValues is null.</exception>
    /// <exception cref="ArgumentException">Thrown when arrays are empty, have fewer than 2 elements, or have different lengths.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes area under curve using trapezoidal rule from x,y data points")]
    double TrapezoidalFromData(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Computes the definite integral of a polynomial over the interval [a, b] using Simpson's composite rule with 100 subdivisions.
    /// Polynomial coefficients follow the convention: a0 + a1*x + a2*x^2 + ... (constant term at index 0).
    /// </summary>
    /// <param name="coefficients">Polynomial coefficients [a0, a1, a2, ...]. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <param name="a">Lower integration bound. Must be a finite number strictly less than b.</param>
    /// <param name="b">Upper integration bound. Must be a finite number strictly greater than a.</param>
    /// <returns>The definite integral value.</returns>
    /// <exception cref="ArgumentException">Thrown when coefficients is empty or a &gt;= b.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a or b is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Integrates a polynomial (a0 + a1*x + a2*x^2 + ...) over [a, b]")]
    double PolynomialIntegral(List<double> coefficients, double a, double b);
}

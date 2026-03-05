using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Interpolation methods for estimating values between known data points.
/// Exposed as the <c>MathNet_Interpolation</c> module in OutSystems Developer Cloud (ODC).
/// All operations delegate to MathNet.Numerics.Interpolation.
/// </summary>
[OSInterface(Name = "MathNet_Interpolation", Description = "Interpolation methods for estimating values between known data points", IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IInterpolation
{
    /// <summary>
    /// Estimates y at a given x using piecewise linear interpolation (linear spline).
    /// Connects known data points with straight line segments.
    /// </summary>
    /// <param name="xValues">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Known y coordinates. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="x">The x value at which to interpolate. Must be a finite number.</param>
    /// <returns>The interpolated y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when x is NaN or Infinity.</exception>
    /// <exception cref="ArgumentException">Thrown when arrays are empty, have different lengths, or have fewer than 2 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Linear interpolation: estimates y at a given x from known data points")]
    double LinearInterpolate(List<double> xValues, List<double> yValues, double x);

    /// <summary>
    /// Estimates y at a given x using natural cubic spline interpolation.
    /// Produces a smooth curve through all known data points with continuous first and second derivatives.
    /// </summary>
    /// <param name="xValues">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Known y coordinates. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="x">The x value at which to interpolate. Must be a finite number.</param>
    /// <returns>The interpolated y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when x is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Cubic spline interpolation: smooth curve estimation at a given x")]
    double CubicSplineInterpolate(List<double> xValues, List<double> yValues, double x);

    /// <summary>
    /// Estimates y at a given x using Barycentric rational interpolation (Floater-Hormann algorithm).
    /// Suitable for polynomial-like interpolation without Runge's phenomenon.
    /// </summary>
    /// <param name="xValues">Known x coordinates. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Known y coordinates. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="x">The x value at which to interpolate. Must be a finite number.</param>
    /// <returns>The interpolated y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when x is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Polynomial (Barycentric) interpolation at a given x")]
    double PolynomialInterpolate(List<double> xValues, List<double> yValues, double x);

    /// <summary>
    /// Estimates y values at multiple x points using piecewise linear interpolation.
    /// More efficient than calling <see cref="LinearInterpolate"/> in a loop because the spline is built once.
    /// </summary>
    /// <param name="xKnown">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yKnown">Known y coordinates. Must have the same length as <paramref name="xKnown"/>.</param>
    /// <param name="xPoints">X values at which to interpolate. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>A list of interpolated y values with the same length as <paramref name="xPoints"/>.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Linear interpolation for multiple x points. Returns list of y values")]
    List<double> BulkLinearInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints);

    /// <summary>
    /// Estimates y values at multiple x points using natural cubic spline interpolation.
    /// More efficient than calling <see cref="CubicSplineInterpolate"/> in a loop because the spline is built once.
    /// </summary>
    /// <param name="xKnown">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yKnown">Known y coordinates. Must have the same length as <paramref name="xKnown"/>.</param>
    /// <param name="xPoints">X values at which to interpolate. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>A list of interpolated y values with the same length as <paramref name="xPoints"/>.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Cubic spline interpolation for multiple x points. Returns list of y values")]
    List<double> BulkCubicSplineInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints);
}

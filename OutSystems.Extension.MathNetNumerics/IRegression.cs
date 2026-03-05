using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Regression analysis, curve fitting, and trend prediction.
/// Exposed as the <c>MathNet_Regression</c> module in OutSystems Developer Cloud (ODC).
/// All operations delegate to MathNet.Numerics.LinearRegression and MathNet.Numerics.Fit.
/// </summary>
[OSInterface(Name = "MathNet_Regression", Description = "Regression, curve fitting, and trend prediction", IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IRegression
{
    /// <summary>
    /// Performs ordinary least squares linear regression on paired x,y data.
    /// Fits the model y = intercept + slope * x.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <returns>A <see cref="LinearRegressionResult"/> containing the intercept and slope.</returns>
    /// <exception cref="ArgumentException">Thrown when arrays are empty, have different lengths, or have fewer than 2 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Linear regression y = a + bx. Returns intercept and slope")]
    LinearRegressionResult LinearRegression(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Predicts a y value for a given x using linear regression fitted on the provided data.
    /// Internally fits y = intercept + slope * x, then evaluates at xPredict.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="xPredict">The x value at which to predict y. Must be a finite number.</param>
    /// <returns>The predicted y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when xPredict is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Predicts y for a given x using linear regression on provided data")]
    double LinearPredict(List<double> xValues, List<double> yValues, double xPredict);

    /// <summary>
    /// Performs polynomial regression of a specified order.
    /// Fits the model y = a0 + a1*x + a2*x^2 + ... + an*x^n.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least (order + 1) elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="order">Polynomial order (degree). Must be positive and less than the number of data points.</param>
    /// <returns>A list of coefficients [a0, a1, a2, ...] with (order + 1) elements.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when order is not positive.</exception>
    /// <exception cref="ArgumentException">Thrown when data has fewer than (order + 1) elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Polynomial regression of given order. Returns coefficients list (a0, a1, a2, ...)")]
    List<double> PolynomialRegression(List<double> xValues, List<double> yValues, int order);

    /// <summary>
    /// Predicts a y value for a given x using polynomial regression of the specified order.
    /// Internally fits the polynomial, then evaluates at xPredict using Horner's method.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least (order + 1) elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="order">Polynomial order (degree). Must be positive.</param>
    /// <param name="xPredict">The x value at which to predict y. Must be a finite number.</param>
    /// <returns>The predicted y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when order is not positive or xPredict is NaN/Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Predicts y for a given x using polynomial regression of specified order")]
    double PolynomialPredict(List<double> xValues, List<double> yValues, int order, double xPredict);

    /// <summary>
    /// Computes the R-squared (coefficient of determination) for a linear regression fit.
    /// Values range from 0 (no fit) to 1 (perfect fit).
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <returns>R-squared value in the range [0, 1]. A value of 1 indicates a perfect linear fit.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes R-squared (coefficient of determination) for linear regression fit")]
    double RSquared(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Fits an exponential model y = A * e^(B*x) to the provided data.
    /// Uses logarithmic linearization internally.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>. All y values must be positive.</param>
    /// <returns>A <see cref="TwoParameterFitResult"/> where A is the multiplier and B is the rate.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Exponential fit y = A*e^(B*x). Returns parameters A and B")]
    TwoParameterFitResult ExponentialFit(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Fits a power model y = A * x^B to the provided data.
    /// Uses logarithmic linearization internally.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. All x values must be positive. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>. All y values must be positive.</param>
    /// <returns>A <see cref="TwoParameterFitResult"/> where A is the coefficient and B is the power exponent.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Power fit y = A*x^B. Returns parameters A and B")]
    TwoParameterFitResult PowerFit(List<double> xValues, List<double> yValues);
}

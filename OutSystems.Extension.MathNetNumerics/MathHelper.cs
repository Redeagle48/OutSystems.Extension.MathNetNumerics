namespace OutSystems.Extension.MathNetNumerics;

internal static class MathHelper
{
    internal const int MaxCollectionSize = 10_000_000;

    internal static double EvaluatePolynomial(IReadOnlyList<double> coefficients, double x)
    {
        double result = 0;
        for (int i = coefficients.Count - 1; i >= 0; i--)
            result = result * x + coefficients[i];
        return result;
    }

    internal static void ValidateNotNullOrEmpty(IReadOnlyCollection<double> values, string paramName)
    {
        ArgumentNullException.ThrowIfNull(values, paramName);
        if (values.Count == 0)
            throw new ArgumentException("List must not be empty.", paramName);
        if (values.Count > MaxCollectionSize)
            throw new ArgumentOutOfRangeException(paramName, values.Count, $"{paramName} must not exceed {MaxCollectionSize} elements.");
    }

    internal static void ValidateMatchingLengths(IReadOnlyCollection<double> a, string aName, IReadOnlyCollection<double> b, string bName)
    {
        if (a.Count != b.Count)
            throw new ArgumentException($"{aName} and {bName} must have the same number of elements. Got {a.Count} and {b.Count}.");
    }

    internal static void ValidateMinCount(IReadOnlyCollection<double> values, int minCount, string paramName)
    {
        if (values.Count < minCount)
            throw new ArgumentException($"{paramName} must have at least {minCount} elements. Got {values.Count}.", paramName);
    }

    internal static void ValidateFinite(double value, string paramName)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} must be a finite number.");
    }

    internal static void ValidatePositive(double value, string paramName)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
            throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} must be greater than zero.");
    }

    internal static void ValidatePositive(int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} must be greater than zero.");
    }

    internal static void ValidateNonNegative(int value, string paramName)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} must not be negative.");
    }

    internal static void ValidateNonNegative(double value, string paramName)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value < 0)
            throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} must be a non-negative finite number.");
    }

    internal static void ValidateRange(double value, double min, double max, string paramName)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} must be between {min} and {max}.");
    }

    internal static void ValidateExclusiveRange(double value, double min, double max, string paramName)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value <= min || value >= max)
            throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} must be between {min} and {max} (exclusive).");
    }
}

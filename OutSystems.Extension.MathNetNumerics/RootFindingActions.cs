using MathNet.Numerics.RootFinding;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Implements root-finding algorithms and break-even analysis.
/// </summary>
public class RootFindingActions : IRootFinding
{
    /// <inheritdoc />
    public double BisectionRoot(List<double> coefficients, double lowerBound, double upperBound)
    {
        MathHelper.ValidateNotNullOrEmpty(coefficients, nameof(coefficients));
        MathHelper.ValidateFinite(lowerBound, nameof(lowerBound));
        MathHelper.ValidateFinite(upperBound, nameof(upperBound));
        if (lowerBound >= upperBound)
            throw new ArgumentException("lowerBound must be less than upperBound.");
        return Bisection.FindRoot(x => MathHelper.EvaluatePolynomial(coefficients, x), lowerBound, upperBound);
    }

    /// <inheritdoc />
    public double BrentRoot(List<double> coefficients, double lowerBound, double upperBound)
    {
        MathHelper.ValidateNotNullOrEmpty(coefficients, nameof(coefficients));
        MathHelper.ValidateFinite(lowerBound, nameof(lowerBound));
        MathHelper.ValidateFinite(upperBound, nameof(upperBound));
        if (lowerBound >= upperBound)
            throw new ArgumentException("lowerBound must be less than upperBound.");
        return Brent.FindRoot(x => MathHelper.EvaluatePolynomial(coefficients, x), lowerBound, upperBound);
    }

    /// <inheritdoc />
    public double BreakEvenQuantity(double pricePerUnit, double fixedCost, double variableCostPerUnit)
    {
        MathHelper.ValidateFinite(pricePerUnit, nameof(pricePerUnit));
        MathHelper.ValidateFinite(fixedCost, nameof(fixedCost));
        MathHelper.ValidateFinite(variableCostPerUnit, nameof(variableCostPerUnit));
        MathHelper.ValidateNonNegative(fixedCost, nameof(fixedCost));
        double margin = pricePerUnit - variableCostPerUnit;
        if (margin <= 0)
            throw new ArgumentException("Price per unit must be greater than variable cost per unit.");
        return fixedCost / margin;
    }
}

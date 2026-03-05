namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class IntegrationValidationTests
{
    private readonly IntegrationActions _sut = new();

    [Fact]
    public void TrapezoidalFromData_EmptyLists_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.TrapezoidalFromData(new List<double>(), new List<double>()));
    }

    [Fact]
    public void TrapezoidalFromData_SinglePoint_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.TrapezoidalFromData(new List<double> { 1 }, new List<double> { 2 }));
    }

    [Fact]
    public void TrapezoidalFromData_MismatchedLengths_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.TrapezoidalFromData(new List<double> { 1, 2, 3 }, new List<double> { 1, 2 }));
    }

    [Fact]
    public void TrapezoidalFromData_NullX_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.TrapezoidalFromData(null!, new List<double> { 1, 2 }));
    }

    [Fact]
    public void PolynomialIntegral_EmptyCoefficients_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PolynomialIntegral(new List<double>(), 0, 1));
    }

    [Fact]
    public void PolynomialIntegral_LowerBoundGreaterThanUpper_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PolynomialIntegral(new List<double> { 1 }, 5, 2));
    }

    [Fact]
    public void PolynomialIntegral_EqualBounds_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PolynomialIntegral(new List<double> { 1 }, 3, 3));
    }

    [Fact]
    public void PolynomialIntegral_NaNLowerBound_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.PolynomialIntegral(new List<double> { 1 }, double.NaN, 1));
    }

    [Fact]
    public void PolynomialIntegral_InfinityUpperBound_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.PolynomialIntegral(new List<double> { 1 }, 0, double.PositiveInfinity));
    }
}

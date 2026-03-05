namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class RootFindingValidationTests
{
    private readonly RootFindingActions _sut = new();

    [Fact]
    public void BisectionRoot_EmptyCoefficients_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BisectionRoot(new List<double>(), 0, 1));
    }

    [Fact]
    public void BisectionRoot_NullCoefficients_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.BisectionRoot(null!, 0, 1));
    }

    [Fact]
    public void BisectionRoot_LowerGreaterThanUpper_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BisectionRoot(new List<double> { -4, 0, 1 }, 5, 1));
    }

    [Fact]
    public void BisectionRoot_EqualBounds_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BisectionRoot(new List<double> { -4, 0, 1 }, 2, 2));
    }

    [Fact]
    public void BrentRoot_EmptyCoefficients_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BrentRoot(new List<double>(), 0, 1));
    }

    [Fact]
    public void BrentRoot_LowerGreaterThanUpper_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BrentRoot(new List<double> { -4, 0, 1 }, 5, 1));
    }

    [Fact]
    public void BreakEvenQuantity_ZeroFixedCost_ReturnsZero()
    {
        var result = _sut.BreakEvenQuantity(10, 0, 5);
        Assert.Equal(0, result, 4);
    }

    [Fact]
    public void BisectionRoot_NaNLowerBound_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.BisectionRoot(new List<double> { -4, 0, 1 }, double.NaN, 3));
    }

    [Fact]
    public void BrentRoot_InfinityUpperBound_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.BrentRoot(new List<double> { -4, 0, 1 }, 0, double.PositiveInfinity));
    }

    [Fact]
    public void BreakEvenQuantity_NaNPrice_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.BreakEvenQuantity(double.NaN, 1000, 5));
    }

    [Fact]
    public void BreakEvenQuantity_NegativeFixedCost_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.BreakEvenQuantity(10, -100, 5));
    }
}

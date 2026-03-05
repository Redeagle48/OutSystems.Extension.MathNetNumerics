namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class RegressionValidationTests
{
    private readonly RegressionActions _sut = new();

    [Fact]
    public void LinearRegression_EmptyLists_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.LinearRegression(new List<double>(), new List<double>()));
    }

    [Fact]
    public void LinearRegression_NullX_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.LinearRegression(null!, new List<double> { 1, 2 }));
    }

    [Fact]
    public void LinearRegression_MismatchedLengths_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.LinearRegression(new List<double> { 1, 2, 3 }, new List<double> { 1, 2 }));
    }

    [Fact]
    public void LinearRegression_SinglePoint_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.LinearRegression(new List<double> { 1 }, new List<double> { 1 }));
    }

    [Fact]
    public void PolynomialRegression_ZeroOrder_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.PolynomialRegression(new List<double> { 1, 2, 3 }, new List<double> { 1, 4, 9 }, 0));
    }

    [Fact]
    public void PolynomialRegression_OrderExceedsPoints_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PolynomialRegression(new List<double> { 1, 2 }, new List<double> { 1, 4 }, 2));
    }

    [Fact]
    public void ExponentialFit_SinglePoint_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.ExponentialFit(new List<double> { 1 }, new List<double> { 2 }));
    }

    [Fact]
    public void PowerFit_EmptyLists_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PowerFit(new List<double>(), new List<double>()));
    }

    [Fact]
    public void RSquared_MismatchedLengths_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.RSquared(new List<double> { 1, 2, 3 }, new List<double> { 1 }));
    }

    [Fact]
    public void LinearPredict_NaNXPredict_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.LinearPredict(new List<double> { 1, 2, 3 }, new List<double> { 2, 4, 6 }, double.NaN));
    }

    [Fact]
    public void PolynomialPredict_InfinityXPredict_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.PolynomialPredict(new List<double> { 1, 2, 3 }, new List<double> { 1, 4, 9 }, 2, double.PositiveInfinity));
    }
}

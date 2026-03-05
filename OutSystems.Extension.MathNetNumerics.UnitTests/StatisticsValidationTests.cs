namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class StatisticsValidationTests
{
    private readonly StatisticsActions _sut = new();

    [Fact]
    public void Mean_EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Mean(new List<double>()));
    }

    [Fact]
    public void Mean_NullList_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.Mean(null!));
    }

    [Fact]
    public void Variance_SingleElement_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Variance(new List<double> { 1 }));
    }

    [Fact]
    public void StandardDeviation_SingleElement_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.StandardDeviation(new List<double> { 1 }));
    }

    [Fact]
    public void Skewness_TwoElements_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Skewness(new List<double> { 1, 2 }));
    }

    [Fact]
    public void Kurtosis_ThreeElements_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Kurtosis(new List<double> { 1, 2, 3 }));
    }

    [Fact]
    public void Percentile_NegativeValue_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.Percentile(new List<double> { 1, 2, 3 }, -1));
    }

    [Fact]
    public void Percentile_Above100_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.Percentile(new List<double> { 1, 2, 3 }, 101));
    }

    [Fact]
    public void Percentile_EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.Percentile(new List<double>(), 50));
    }

    [Fact]
    public void PearsonCorrelation_MismatchedLengths_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PearsonCorrelation(new List<double> { 1, 2, 3 }, new List<double> { 1, 2 }));
    }

    [Fact]
    public void PearsonCorrelation_SingleElement_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PearsonCorrelation(new List<double> { 1 }, new List<double> { 2 }));
    }

    [Fact]
    public void Summary_EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Summary(new List<double>()));
    }

    [Fact]
    public void Summary_TooFewElements_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Summary(new List<double> { 1, 2, 3 }));
    }

    [Fact]
    public void Min_EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Min(new List<double>()));
    }

    [Fact]
    public void Max_EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.Max(new List<double>()));
    }
}

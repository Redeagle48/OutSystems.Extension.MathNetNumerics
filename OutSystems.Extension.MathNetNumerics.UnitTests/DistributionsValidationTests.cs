namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class DistributionsValidationTests
{
    private readonly DistributionsActions _sut = new();

    [Fact]
    public void NormalPdf_ZeroStddev_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalPdf(0, 0, 0));
    }

    [Fact]
    public void NormalPdf_NegativeStddev_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalPdf(0, -1, 0));
    }

    [Fact]
    public void NormalCdf_NegativeStddev_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalCdf(0, -1, 0));
    }

    [Fact]
    public void NormalInverseCdf_ProbabilityZero_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalInverseCdf(0, 1, 0));
    }

    [Fact]
    public void NormalInverseCdf_ProbabilityOne_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalInverseCdf(0, 1, 1));
    }

    [Fact]
    public void NormalInverseCdf_ProbabilityNegative_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalInverseCdf(0, 1, -0.5));
    }

    [Fact]
    public void NormalSample_ZeroCount_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalSample(0, 1, 0));
    }

    [Fact]
    public void NormalSample_NegativeCount_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalSample(0, 1, -5));
    }

    [Fact]
    public void NormalSample_NegativeStddev_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalSample(0, -1, 10));
    }

    [Fact]
    public void ExponentialCdf_ZeroRate_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.ExponentialCdf(0, 1));
    }

    [Fact]
    public void ExponentialCdf_NegativeRate_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.ExponentialCdf(-1, 1));
    }

    [Fact]
    public void ExponentialInverseCdf_ProbabilityOutOfRange_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.ExponentialInverseCdf(1, 1.5));
    }

    [Fact]
    public void PoissonProbability_ZeroMean_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PoissonProbability(0, 1));
    }

    [Fact]
    public void PoissonProbability_NegativeK_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PoissonProbability(1, -1));
    }

    [Fact]
    public void BinomialProbability_ProbabilityAboveOne_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.BinomialProbability(1.5, 10, 5));
    }

    [Fact]
    public void BinomialProbability_NegativeProbability_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.BinomialProbability(-0.1, 10, 5));
    }

    [Fact]
    public void BinomialProbability_KGreaterThanN_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.BinomialProbability(0.5, 10, 11));
    }

    [Fact]
    public void BinomialProbability_NegativeK_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.BinomialProbability(0.5, 10, -1));
    }

    [Fact]
    public void BinomialCdf_NZero_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.BinomialCdf(0.5, 0, 0));
    }

    [Fact]
    public void StudentTInverseCdf_ZeroDf_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.StudentTInverseCdf(0, 0.5));
    }

    [Fact]
    public void StudentTInverseCdf_ProbabilityOutOfRange_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.StudentTInverseCdf(10, 0));
    }

    [Fact]
    public void ChiSquaredCdf_ZeroDf_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.ChiSquaredCdf(0, 1));
    }

    [Fact]
    public void NormalPdf_NaNStddev_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalPdf(0, double.NaN, 0));
    }

    [Fact]
    public void NormalPdf_NaNMean_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalPdf(double.NaN, 1, 0));
    }

    [Fact]
    public void NormalPdf_InfinityX_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalPdf(0, 1, double.PositiveInfinity));
    }

    [Fact]
    public void NormalInverseCdf_NaNProbability_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NormalInverseCdf(0, 1, double.NaN));
    }

    [Fact]
    public void ExponentialCdf_NaNRate_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.ExponentialCdf(double.NaN, 1));
    }

    [Fact]
    public void ExponentialCdf_InfinityX_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.ExponentialCdf(1, double.NegativeInfinity));
    }

    [Fact]
    public void StudentTInverseCdf_NaNDf_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.StudentTInverseCdf(double.NaN, 0.5));
    }

    [Fact]
    public void BinomialProbability_NaNProbability_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.BinomialProbability(double.NaN, 10, 5));
    }

    [Fact]
    public void ChiSquaredCdf_InfinityX_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.ChiSquaredCdf(5, double.PositiveInfinity));
    }
}

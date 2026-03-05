namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class FinancialValidationTests
{
    private readonly FinancialActions _sut = new();

    [Fact]
    public void FutureValue_NegativePeriods_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.FutureValue(1000, 0.05, -1));
    }

    [Fact]
    public void PresentValue_NegativePeriods_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PresentValue(1000, 0.05, -1));
    }

    [Fact]
    public void PresentValue_RateMinusOne_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.PresentValue(1000, -1, 5));
    }

    [Fact]
    public void NetPresentValue_EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.NetPresentValue(0.1, new List<double>()));
    }

    [Fact]
    public void NetPresentValue_NullList_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.NetPresentValue(0.1, null!));
    }

    [Fact]
    public void IRR_EmptyList_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.InternalRateOfReturn(new List<double>()));
    }

    [Fact]
    public void IRR_SingleElement_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.InternalRateOfReturn(new List<double> { -100 }));
    }

    [Fact]
    public void IRR_NonConvergent_Throws()
    {
        // All positive cash flows — no sign change, no real IRR
        Assert.Throws<InvalidOperationException>(() =>
            _sut.InternalRateOfReturn(new List<double> { 100, 100, 100 }));
    }

    [Fact]
    public void PaymentAmount_ZeroPeriods_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PaymentAmount(10000, 0.05, 0));
    }

    [Fact]
    public void PaymentAmount_NegativePeriods_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PaymentAmount(10000, 0.05, -1));
    }

    [Fact]
    public void CompoundInterest_ZeroCompoundings_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.CompoundInterest(1000, 0.05, 0, 10));
    }

    [Fact]
    public void CompoundInterest_NegativeYears_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.CompoundInterest(1000, 0.05, 12, -1));
    }

    [Fact]
    public void StraightLineDepreciation_ZeroLife_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.StraightLineDepreciation(10000, 1000, 0));
    }

    [Fact]
    public void StraightLineDepreciation_NegativeLife_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.StraightLineDepreciation(10000, 1000, -5));
    }

    [Fact]
    public void AmortizationSchedule_ZeroPeriods_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.AmortizationSchedule(10000, 0.06, 0));
    }

    [Fact]
    public void AmortizationSchedule_PrincipalSumsExactly()
    {
        // Test with a 360-month mortgage to verify no rounding drift
        var schedule = _sut.AmortizationSchedule(200000, 0.06, 360);
        decimal totalPrincipal = schedule.Sum(e => e.PrincipalPortion);
        Assert.Equal(200000m, totalPrincipal, 2);
        Assert.Equal(0m, schedule.Last().RemainingBalance);
    }

    [Fact]
    public void PaymentAmount_VerySmallRate_ReturnsCorrectly()
    {
        // Rate so small it's essentially zero
        var result = _sut.PaymentAmount(12000, 1e-15, 12);
        Assert.Equal(1000, result, 2);
    }

    [Fact]
    public void FutureValue_NaNRate_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.FutureValue(1000, double.NaN, 10));
    }

    [Fact]
    public void FutureValue_InfinityPresentValue_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.FutureValue(double.PositiveInfinity, 0.05, 10));
    }

    [Fact]
    public void PresentValue_NaNRate_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PresentValue(1000, double.NaN, 5));
    }

    [Fact]
    public void NetPresentValue_NaNDiscountRate_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.NetPresentValue(double.NaN, new List<double> { -100, 50, 60 }));
    }

    [Fact]
    public void PaymentAmount_NaNPrincipal_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.PaymentAmount(double.NaN, 0.05, 12));
    }

    [Fact]
    public void CompoundInterest_InfinityRate_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.CompoundInterest(1000, double.PositiveInfinity, 12, 10));
    }

    [Fact]
    public void StraightLineDepreciation_NaNCost_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.StraightLineDepreciation(double.NaN, 1000, 5));
    }

    [Fact]
    public void AmortizationSchedule_NaNPrincipal_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.AmortizationSchedule(double.NaN, 0.06, 12));
    }

    [Fact]
    public void AmortizationSchedule_ExceedsMaxPeriods_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.AmortizationSchedule(10000, 0.06, 13000));
    }
}

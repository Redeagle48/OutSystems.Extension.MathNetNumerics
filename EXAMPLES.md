# Code Examples

Minimal, self-contained C# examples for each module. All examples can be run in a .NET 8 console application with a reference to this library.

## Financial

```csharp
var financial = new FinancialActions();

// Monthly mortgage payment: $200,000 loan at 6% for 30 years
double payment = financial.PaymentAmount(
    principal: 200000,
    annualRate: 0.06,     // 6% expressed as decimal
    totalPeriods: 360     // 30 years * 12 months
);
// payment ≈ 1199.10

// Future value of $10,000 at 5% annual rate for 10 years
double fv = financial.FutureValue(
    presentValue: 10000,
    annualRate: 0.05,
    periods: 10
);
// fv ≈ 16288.95

// Net Present Value of an investment
double npv = financial.NetPresentValue(
    discountRate: 0.10,   // 10% discount rate
    cashFlows: new List<double> { -100000, 30000, 35000, 40000, 45000 }
);
// npv ≈ 16066.35

// Internal Rate of Return
double irr = financial.InternalRateOfReturn(
    cashFlows: new List<double> { -100000, 30000, 35000, 40000, 45000 }
);
// irr ≈ 0.1466 (14.66%)

// Compound interest on $5,000 at 4% compounded monthly for 5 years
double interest = financial.CompoundInterest(
    principal: 5000,
    annualRate: 0.04,
    compoundingsPerYear: 12,
    years: 5
);
// interest ≈ 1104.98

// Full amortization schedule
List<AmortizationScheduleEntry> schedule = financial.AmortizationSchedule(
    principal: 100000,
    annualRate: 0.05,
    totalPeriods: 12
);
// schedule[0].Payment, schedule[0].PrincipalPortion, schedule[0].InterestPortion, etc.
```

## Statistics

```csharp
var stats = new StatisticsActions();

var data = new List<double> { 10, 20, 30, 40, 50 };

double mean = stats.Mean(data);               // 30.0
double median = stats.Median(data);           // 30.0
double variance = stats.Variance(data);       // 250.0
double stddev = stats.StandardDeviation(data);// ≈ 15.81
double min = stats.Min(data);                 // 10.0
double max = stats.Max(data);                 // 50.0

// 90th percentile
double p90 = stats.Percentile(data, 90);      // ≈ 46.0

// Pearson correlation between two datasets
double correlation = stats.PearsonCorrelation(
    valuesA: new List<double> { 1, 2, 3, 4, 5 },
    valuesB: new List<double> { 2, 4, 6, 8, 10 }
);
// correlation = 1.0 (perfect positive correlation)

// Full summary in one call (requires at least 4 elements)
var data4 = new List<double> { 10, 20, 30, 40, 50 };
StatisticsSummary summary = stats.Summary(data4);
// summary.Mean, summary.Median, summary.Variance, summary.Skewness, summary.Kurtosis, etc.
```

## Distributions

```csharp
var dist = new DistributionsActions();

// Normal distribution: probability of scoring below 85 on a test (mean=75, stddev=10)
double prob = dist.NormalCdf(mean: 75, stddev: 10, x: 85);
// prob ≈ 0.8413 (84.13%)

// Normal PDF at x = 0 for standard normal
double density = dist.NormalPdf(mean: 0, stddev: 1, x: 0);
// density ≈ 0.3989

// Normal inverse CDF: what score is at the 95th percentile?
double quantile = dist.NormalInverseCdf(mean: 75, stddev: 10, p: 0.95);
// quantile ≈ 91.45

// Generate 1,000 random samples from N(100, 15)
List<double> samples = dist.NormalSample(mean: 100, stddev: 15, count: 1000);

// Poisson: probability of exactly 3 events when mean rate is 5
double poissonP = dist.PoissonProbability(mean: 5, k: 3);
// poissonP ≈ 0.1404

// Binomial: probability of exactly 7 heads in 10 fair coin flips
double binomP = dist.BinomialProbability(p: 0.5, n: 10, k: 7);
// binomP ≈ 0.1172

// Student's t critical value for 95% confidence with 29 degrees of freedom
double tCritical = dist.StudentTInverseCdf(degreesOfFreedom: 29, probability: 0.975);
// tCritical ≈ 2.045

// Chi-squared CDF for goodness-of-fit
double chiP = dist.ChiSquaredCdf(degreesOfFreedom: 5, x: 11.07);
// chiP ≈ 0.95
```

## Regression

```csharp
var regression = new RegressionActions();

var x = new List<double> { 1, 2, 3, 4, 5 };
var y = new List<double> { 2.1, 3.9, 6.2, 7.8, 10.1 };

// Linear regression
LinearRegressionResult result = regression.LinearRegression(x, y);
// result.Intercept ≈ 0.02, result.Slope ≈ 2.0

// Predict y at x = 6
double predicted = regression.LinearPredict(x, y, xPredict: 6);
// predicted ≈ 12.02

// R-squared goodness of fit
double r2 = regression.RSquared(x, y);
// r2 ≈ 0.998 (very good fit)

// Polynomial regression (quadratic)
var xq = new List<double> { 1, 2, 3, 4, 5 };
var yq = new List<double> { 1, 4, 9, 16, 25 };
List<double> coefficients = regression.PolynomialRegression(xq, yq, order: 2);
// coefficients ≈ [0, 0, 1] representing y = x^2

// Exponential fit: y = A * e^(B*x)
var xe = new List<double> { 1, 2, 3, 4, 5 };
var ye = new List<double> { 2.7, 7.4, 20.1, 54.6, 148.4 };
TwoParameterFitResult expResult = regression.ExponentialFit(xe, ye);
// expResult.A ≈ 1.0, expResult.B ≈ 1.0
```

## Interpolation

```csharp
var interp = new InterpolationActions();

var xKnown = new List<double> { 0, 1, 2, 3, 4 };
var yKnown = new List<double> { 0, 1, 4, 9, 16 };

// Linear interpolation at x = 1.5
double yLinear = interp.LinearInterpolate(xKnown, yKnown, x: 1.5);
// yLinear = 2.5 (midpoint between 1 and 4)

// Cubic spline interpolation at x = 1.5
double ySpline = interp.CubicSplineInterpolate(xKnown, yKnown, x: 1.5);
// ySpline ≈ 2.25 (closer to true x^2 = 2.25)

// Barycentric polynomial interpolation at x = 1.5
double yPoly = interp.PolynomialInterpolate(xKnown, yKnown, x: 1.5);
// yPoly ≈ 2.25

// Bulk interpolation: evaluate at multiple points at once
var xPoints = new List<double> { 0.5, 1.5, 2.5, 3.5 };
List<double> yBulk = interp.BulkLinearInterpolate(xKnown, yKnown, xPoints);
// yBulk ≈ [0.5, 2.5, 6.5, 12.5]
```

## Integration

```csharp
var integration = new IntegrationActions();

// Area under curve from data points (trapezoidal rule)
var xData = new List<double> { 0, 1, 2, 3, 4 };
var yData = new List<double> { 0, 1, 4, 9, 16 };
double area = integration.TrapezoidalFromData(xData, yData);
// area = 22.0

// Definite integral of polynomial x^2 from 0 to 3
// Coefficients: [0, 0, 1] represents 0 + 0*x + 1*x^2
double integral = integration.PolynomialIntegral(
    coefficients: new List<double> { 0, 0, 1 },
    a: 0,
    b: 3
);
// integral ≈ 9.0 (exact: 3^3/3 = 9)
```

## Root Finding

```csharp
var roots = new RootFindingActions();

// Find root of x^2 - 4 = 0 in [0, 3]
// Coefficients: [-4, 0, 1] represents -4 + 0*x + 1*x^2
double root = roots.BisectionRoot(
    coefficients: new List<double> { -4, 0, 1 },
    lowerBound: 0,
    upperBound: 3
);
// root ≈ 2.0

// Same with Brent's method (faster convergence)
double rootBrent = roots.BrentRoot(
    coefficients: new List<double> { -4, 0, 1 },
    lowerBound: 0,
    upperBound: 3
);
// rootBrent ≈ 2.0

// Break-even analysis: price=$25, fixed costs=$10,000, variable cost=$15
double breakEven = roots.BreakEvenQuantity(
    pricePerUnit: 25,
    fixedCost: 10000,
    variableCostPerUnit: 15
);
// breakEven = 1000 units
```

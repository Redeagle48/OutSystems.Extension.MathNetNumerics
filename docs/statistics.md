# How to Compute Mean, Standard Deviation, and Correlation in OutSystems

The `MathNetNumerics` module provides 11 Statistics server actions for descriptive statistics and correlation analysis in OutSystems Developer Cloud (ODC). All operations delegate to MathNet.Numerics.Statistics.

## Actions

| Action | Description | Minimum Elements |
|--------|-------------|-----------------|
| `Mean` | Arithmetic mean (average) | 1 |
| `Median` | Middle value (average of two for even-length lists) | 1 |
| `Variance` | Sample variance (unbiased, N-1 denominator) | 2 |
| `StandardDeviation` | Sample standard deviation (sqrt of variance) | 2 |
| `Skewness` | Distribution asymmetry measure | 3 |
| `Kurtosis` | Distribution tail heaviness (excess kurtosis; normal = 0) | 4 |
| `Percentile` | Value at a given percentile (0-100) | 1 |
| `PearsonCorrelation` | Pearson correlation coefficient (-1 to +1) | 2 (matched length) |
| `Min` | Minimum value | 1 |
| `Max` | Maximum value | 1 |
| `Summary` | All 8 measures as a `StatisticsSummary` structure | 4 |

## StatisticsSummary Structure

The `Summary` action returns a single structure with all descriptive statistics:

| Field | Type | Description |
|-------|------|-------------|
| `Min` | double | Minimum value |
| `Max` | double | Maximum value |
| `Mean` | double | Arithmetic mean |
| `Median` | double | Median value |
| `StandardDeviation` | double | Sample standard deviation |
| `Variance` | double | Sample variance |
| `Skewness` | double | Skewness (0 = symmetric) |
| `Kurtosis` | double | Excess kurtosis (0 = normal distribution) |

## Input Validation

All list parameters must not be null or empty and cannot exceed 10,000,000 elements. Each action enforces its minimum element count. Percentile must be between 0 and 100 inclusive. Paired datasets for PearsonCorrelation must have the same length.

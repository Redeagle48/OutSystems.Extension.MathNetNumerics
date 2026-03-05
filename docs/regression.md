# How to Perform Regression and Curve Fitting in OutSystems

The `MathNetNumerics` module provides 7 Regression server actions for regression analysis, curve fitting, and prediction in OutSystems Developer Cloud (ODC).

## Actions

| Action | Model | Returns |
|--------|-------|---------|
| `LinearRegression` | y = intercept + slope * x | `LinearRegressionResult` |
| `LinearPredict` | y = intercept + slope * x | Predicted y (double) |
| `PolynomialRegression` | y = a0 + a1*x + a2*x^2 + ... | List of coefficients |
| `PolynomialPredict` | y = a0 + a1*x + a2*x^2 + ... | Predicted y (double) |
| `RSquared` | Linear | Coefficient of determination (0 to 1) |
| `ExponentialFit` | y = A * e^(B*x) | `TwoParameterFitResult` |
| `PowerFit` | y = A * x^B | `TwoParameterFitResult` |

## Return Structures

**LinearRegressionResult:** `Intercept` (double), `Slope` (double)

**TwoParameterFitResult:** `A` (coefficient/multiplier), `B` (rate/exponent)

## How R-Squared Works

R-squared (coefficient of determination) measures how well the linear model fits the data:
- `1.0` = perfect fit (all points lie exactly on the regression line)
- `0.0` = the model explains none of the variance
- Values between 0.8 and 1.0 generally indicate a strong linear relationship

## Input Validation

Both `xValues` and `yValues` must have the same length and at least 2 elements (or `order + 1` for polynomial regression). Maximum 10,000,000 elements. Prediction x-values must be finite (no NaN or Infinity). Polynomial order must be positive.

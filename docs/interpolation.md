# How to Interpolate Data Points in OutSystems

The `MathNetNumerics` module provides 5 Interpolation server actions for estimating values between known data points in OutSystems Developer Cloud (ODC).

## Actions

| Action | Method | Best For |
|--------|--------|----------|
| `LinearInterpolate` | Piecewise linear spline | Fast, simple estimation between points |
| `CubicSplineInterpolate` | Natural cubic spline | Smooth curves with continuous 1st and 2nd derivatives |
| `PolynomialInterpolate` | Barycentric (Floater-Hormann) | Polynomial-like fit without Runge's phenomenon |
| `BulkLinearInterpolate` | Linear spline (batch) | Multiple evaluation points at once |
| `BulkCubicSplineInterpolate` | Cubic spline (batch) | Multiple smooth evaluations at once |

## When to Use Each Method

- **Linear interpolation** is fastest and works well when data points are closely spaced or when the underlying function is approximately linear between points.
- **Cubic spline interpolation** produces a smooth curve and is preferred when visual smoothness matters or when the underlying function is smooth.
- **Polynomial interpolation** (Floater-Hormann) avoids the oscillation issues of classical polynomial interpolation and works well for moderately sized datasets.
- **Bulk methods** are more efficient than calling single-point methods in a loop because the interpolation model is built once and evaluated multiple times.

## Input Validation

Known x and y arrays must have at least 2 elements and the same length. Maximum 10,000,000 elements. The x value for single-point interpolation must be finite (no NaN or Infinity). Bulk x-point lists must not be null or empty.

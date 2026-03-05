# How to Compute Numerical Integrals in OutSystems

The `MathNet_Integration` module provides 2 server actions for definite integration and area computation in OutSystems Developer Cloud (ODC).

## Actions

### TrapezoidalFromData

Computes the area under a curve from paired x,y data points using the trapezoidal rule. Sums the areas of trapezoids formed between consecutive data points.

- **Parameters:** `xValues` (List), `yValues` (List) — must have at least 2 elements and the same length
- **Returns:** Approximate area (double)
- **Accuracy:** Exact for linear segments; approximation error is O(h^2) where h is the spacing between x values

### PolynomialIntegral

Computes the definite integral of a polynomial over [a, b] using Simpson's composite rule with 100 subdivisions.

- **Parameters:** `coefficients` (List, convention: a0 + a1*x + a2*x^2 + ...), `a` (lower bound), `b` (upper bound)
- **Returns:** Definite integral value (double)
- **Constraint:** `a` must be strictly less than `b`

## Input Validation

Arrays must not be null or empty and cannot exceed 10,000,000 elements. Paired arrays must have matching lengths with at least 2 elements. Integration bounds `a` and `b` must be finite (no NaN or Infinity) and `a < b`.

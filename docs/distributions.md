# How to Evaluate Probability Distributions in OutSystems

The `MathNet_Distributions` module provides 12 server actions covering six probability distributions in OutSystems Developer Cloud (ODC): Normal, Exponential, Poisson, Binomial, Student's t, and Chi-squared.

## Normal Distribution

| Action | Description | Key Parameters |
|--------|-------------|---------------|
| `NormalPdf` | Probability density at x | `mean`, `stddev` (> 0), `x` |
| `NormalCdf` | Cumulative probability P(X <= x) | `mean`, `stddev` (> 0), `x` |
| `NormalInverseCdf` | Quantile: x where P(X <= x) = p | `mean`, `stddev` (> 0), `p` (0,1 exclusive) |
| `NormalSample` | Generate random samples | `mean`, `stddev` (> 0), `count` (1 to 1,000,000) |

## Exponential Distribution

| Action | Description | Key Parameters |
|--------|-------------|---------------|
| `ExponentialCdf` | Cumulative probability P(X <= x) | `rate` (> 0), `x` |
| `ExponentialInverseCdf` | Quantile function | `rate` (> 0), `p` (0,1 exclusive) |

## Poisson Distribution

| Action | Description | Key Parameters |
|--------|-------------|---------------|
| `PoissonProbability` | P(X = k) for exactly k events | `mean` (> 0), `k` (>= 0) |
| `PoissonCdf` | P(X <= k) for k or fewer events | `mean` (> 0), `k` (>= 0) |

## Binomial Distribution

| Action | Description | Key Parameters |
|--------|-------------|---------------|
| `BinomialProbability` | P(X = k) for k successes in n trials | `p` [0,1], `n` (> 0), `k` [0,n] |
| `BinomialCdf` | P(X <= k) | `p` [0,1], `n` (> 0), `k` [0,n] |

## Hypothesis Testing

| Action | Description | Key Parameters |
|--------|-------------|---------------|
| `StudentTInverseCdf` | t critical value | `degreesOfFreedom` (> 0), `probability` (0,1 exclusive) |
| `ChiSquaredCdf` | Chi-squared cumulative probability | `degreesOfFreedom` (> 0), `x` |

## Input Validation

All double parameters must be finite (no NaN or Infinity). Standard deviation and rate must be positive. Probabilities for inverse CDF functions must be strictly between 0 and 1. Binomial k must be between 0 and n.

# Spherical Coordinates Library

A pure C# library for working with spherical coordinates. No external dependencies except for Microsoft's unit testing, built for .NET 9.0.

This code is free to use under the MIT.

## What Are Spherical Coordinates?
Spherical coordinates provide a way to represent points in three-dimensional space using a radius, an azimuthal angle, and a polar angle. Unlike Cartesian coordinates (x, y, z), which define a point by horizontal and vertical displacement, spherical coordinates define a point as:

- **Radius (r)**: The distance from the origin to the point.
- **Theta (θ)**: The azimuthal angle (in radians) measured counterclockwise from the positive x-axis in the xy-plane.
- **Phi (φ)**: The polar angle (in radians) measured from the positive z-axis.

### Direction of Theta (θ) and Phi (φ)
- Theta (θ) is measured in radians and represents the angle around the z-axis.
- Phi (φ) is measured in radians from the positive z-axis downward.
- The angle ranges are typically:
  - **θ** in [0, 2π)
  - **φ** in [0, π]

## Functionality of the `Spherical` Class
This `Spherical` class provides methods to work with spherical coordinates, including conversions, measurements, and calculations.

### Constructor
```csharp
public Spherical(double radius, double theta, double phi)
```
- Initializes a `Spherical` object with a specified radius, azimuthal angle, and polar angle.

### Conversion Methods
#### `FromCartesian(Vector3 point)`
```csharp
public static Spherical FromCartesian(Vector3 point)
```
- Converts a Cartesian coordinate (x, y, z) into spherical coordinates.
- Returns `NaN` values if the input contains `NaN` values.
- Uses the formula:
  - `r = sqrt(x² + y² + z²)`
  - `θ = atan2(y, x)`
  - `φ = acos(z / r)`

#### `ToCartesian()`
```csharp
public Vector3 ToCartesian()
```
- Converts a spherical coordinate to Cartesian form (x, y, z).
- Uses the formulas:
  - `x = r * sin(φ) * cos(θ)`
  - `y = r * sin(φ) * sin(θ)`
  - `z = r * cos(φ)`

### Measurement Functions
#### `SurfaceArea(double radius)`
```csharp
public static double SurfaceArea(double radius)
```
- Computes the surface area of a sphere.
- Formula: `Surface Area = 4πr²`.
- Returns `NaN` if the radius is negative.

#### `Volume(double radius)`
```csharp
public static double Volume(double radius)
```
- Computes the volume of a sphere.
- Formula: `Volume = (4/3)πr³`.
- Returns `NaN` if the radius is negative.

#### `GreatCircleDistance(double radius, double phi1, double theta1, double phi2, double theta2)`
```csharp
public static double GreatCircleDistance(double radius, double phi1, double theta1, double phi2, double theta2)
```
- Computes the shortest distance between two points on a sphere.
- Uses the Haversine formula.
- Returns `NaN` if any input is `NaN`.

### Spherical Segments and Zones
#### `SphericalCap(double radius, double height)`
```csharp
public static double SphericalCap(double radius, double height)
```
- Computes the surface area of a spherical cap.
- Formula: `Cap Area = 2πr h`.
- Returns `NaN` if any input is `NaN`.

#### `SphericalZone(double radius, double height1, double height2)`
```csharp
public static double SphericalZone(double radius, double height1, double height2)
```
- Computes the surface area of a spherical zone.
- Formula: `Zone Area = 2πr |h₂ - h₁|`.
- Returns `NaN` if any input is `NaN`.

#### `SphericalSegmentVolume(double radius, double height)`
```csharp
public static double SphericalSegmentVolume(double radius, double height)
```
- Computes the volume of a spherical segment.
- Formula: `Volume = (π h² (3r - h)) / 3`.
- Returns `NaN` if any input is `NaN`.

#### `SphericalSectorVolume(double radius, double phi)`
```csharp
public static double SphericalSectorVolume(double radius, double phi)
```
- Computes the volume of a spherical sector.
- Formula: `Volume = (2/3)πr³(1 - cos(φ))`.
- Returns `NaN` if any input is `NaN`.

## Usage Example
```csharp
Vector3 point = new Vector3(3, 4, 5);
Spherical spherical = Spherical.FromCartesian(point);
Console.WriteLine($"Radius: {spherical.Radius}, Theta: {spherical.Theta}, Phi: {spherical.Phi}");
Vector3 cartesian = spherical.ToCartesian();
Console.WriteLine($"Converted back: ({cartesian.X}, {cartesian.Y}, {cartesian.Z})");
```

This library provides robust mathematical operations for handling spherical coordinates, including conversions, distance calculations, and curvature analysis.

![AI Image](aiimage.jpg)
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>


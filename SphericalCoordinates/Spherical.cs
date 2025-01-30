using System.Numerics;

namespace SphericalCoordinates
{
    public class Spherical
    {
        public readonly double Radius; // Distance from origin (r)
        public readonly double Theta; // Azimuthal angle in XY-plane from X-axis (θ)
        public readonly double Phi; // Polar angle from Z-axis (φ)

        public Spherical(double radius, double theta, double phi)
        {
            Radius = radius;
            Theta = theta;
            Phi = phi;
        }

        public static Vector3 ToCartesian(double radius, double theta, double phi)
        {
            if (double.IsNaN(radius) || double.IsNaN(theta) || double.IsNaN(phi))
                return new Vector3(float.NaN, float.NaN, float.NaN);
            double sinPhi = Math.Sin(phi);
            double x = radius * sinPhi * Math.Cos(theta);
            double y = radius * sinPhi * Math.Sin(theta);
            double z = radius * Math.Cos(phi);
            return new Vector3((float)x, (float)y, (float)z);
        }

        public static Spherical FromCartesian(Vector3 cartesian)
        {
            if (float.IsNaN(cartesian.X) || float.IsNaN(cartesian.Y) || float.IsNaN(cartesian.Z))
                return new Spherical(double.NaN, double.NaN, double.NaN);
            double x = cartesian.X;
            double y = cartesian.Y;
            double z = cartesian.Z;
            double radius = Math.Sqrt(x * x + y * y + z * z);
            if (radius == 0)
                return new Spherical(0, 0, 0);
            double theta = Math.Atan2(y, x);
            double phi = Math.Acos(z / radius);
            return new Spherical(radius, theta, phi);
        }

        public static double SurfaceArea(double radius)
        {
            if (double.IsNaN(radius))
                return double.NaN;
            return 4 * Math.PI * radius * radius;
        }

        public static double Volume(double radius)
        {
            if (double.IsNaN(radius))
                return double.NaN;
            return (4.0 / 3.0) * Math.PI * radius * radius * radius;
        }

        public static double GreatCircleDistance(double radius, double phi1, double theta1, double phi2, double theta2)
        {
            if (double.IsNaN(radius) || double.IsNaN(phi1) || double.IsNaN(theta1) || double.IsNaN(phi2) || double.IsNaN(theta2))
                return double.NaN;

            double sinPhi1 = Math.Sin(phi1);
            double cosPhi1 = Math.Cos(phi1);
            double sinPhi2 = Math.Sin(phi2);
            double cosPhi2 = Math.Cos(phi2);
            double deltaTheta = theta2 - theta1;
            double cosDeltaTheta = Math.Cos(deltaTheta);

            double cosDistance = sinPhi1 * sinPhi2 + cosPhi1 * cosPhi2 * cosDeltaTheta;

            // Prevent floating-point precision errors
            cosDistance = Math.Clamp(cosDistance, -1.0, 1.0);

            double distance = radius * Math.Acos(cosDistance);

            return distance;
        }

        public Vector3 ToCartesianVector()
        {
            return ToCartesian(Radius, Theta, Phi);
        }

        public static double SphericalCap(double radius, double height)
        {
            if (double.IsNaN(radius) || double.IsNaN(height))
                return double.NaN;
            return 2 * Math.PI * radius * height;
        }

        public static double SphericalZone(double radius, double height1, double height2)
        {
            if (double.IsNaN(radius) || double.IsNaN(height1) || double.IsNaN(height2))
                return double.NaN;
            return 2 * Math.PI * radius * Math.Abs(height2 - height1);
        }

        public static double SphericalSegmentVolume(double radius, double height)
        {
            if (double.IsNaN(radius) || double.IsNaN(height))
                return double.NaN;
            return (Math.PI * height * height * (3 * radius - height)) / 3;
        }

        public static double SphericalSectorVolume(double radius, double phi)
        {
            if (double.IsNaN(radius) || double.IsNaN(phi))
                return double.NaN;
            return (2.0 / 3.0) * Math.PI * radius * radius * radius * (1 - Math.Cos(phi));
        }
    }
}

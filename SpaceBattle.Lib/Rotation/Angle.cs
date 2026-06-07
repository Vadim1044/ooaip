namespace SpaceBattle.Lib;

public class Angle
{
    public static int Denominator { get; set; }

    public int Numerator { get; set; }

    public Angle(int numerator, int denominator)
    {
        Denominator = denominator;
        Numerator = ((numerator % Denominator) + Denominator) % Denominator;
    }

    public static Angle operator +(Angle a, Angle b) =>
        new(a.Numerator + b.Numerator, Denominator);

    public static bool operator ==(Angle? a, Angle? b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return a.Numerator == b.Numerator;
    }

    public static bool operator !=(Angle? a, Angle? b) => !(a == b);

    public override bool Equals(object? obj) =>
        obj is Angle other && Numerator == other.Numerator;

    public override int GetHashCode() => Numerator.GetHashCode();

    public static implicit operator double(Angle angle) =>
        (double)angle.Numerator / Denominator * 2 * Math.PI;
}

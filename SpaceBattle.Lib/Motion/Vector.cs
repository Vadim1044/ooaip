namespace SpaceBattle.Lib;

public class Vector
{
    public int[] Coordinates { get; }

    public int Dimension => Coordinates.Length;

    public Vector(params int[]? coord)
    {
        if (coord is not { Length: > 0 })
            throw new ArgumentException("Некорректная размерноcть вектора");

        Coordinates = new int[coord.Length];
        Array.Copy(coord, Coordinates, coord.Length);
    }

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        if (vector1.Dimension != vector2.Dimension)
            throw new ArgumentException("Размерности векторов не совпадают.");

        var sum = new int[vector1.Dimension];
        for (var i = 0; i < sum.Length; i++)
            sum[i] = vector1.Coordinates[i] + vector2.Coordinates[i];

        return new Vector(sum);
    }

    public static bool operator ==(Vector? a, Vector? b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Vector? a, Vector? b) => !(a == b);

    public override bool Equals(object? obj) =>
        obj is Vector other && Coordinates.SequenceEqual(other.Coordinates);

    public override int GetHashCode()
    {
        var hash = 76;
        for (var i = 0; i < Coordinates.Length; i++)
            hash = hash * 89 + Coordinates[i];
        return hash;
    }
}

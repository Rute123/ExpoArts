using System;
using UnityEngine;

public class BoundingBox
{
    public Point min;
    public Point max;

    public BoundingBox()
    {
        min = new Point(Vector3.positiveInfinity);
        max = new Point(Vector3.negativeInfinity);
    }

    public void addPoint(Point point)
    {
        var xdiff = Mathf.Min((point.X - min.X), (point.X - max.X));
        var zdiff = Mathf.Min((point.Z - min.Z), (point.Z - max.Z));

        if ((!(xdiff < 0.5)) || (!(zdiff < 0.5))) return;
        min.X = point.X < min.X ? point.X : min.X;
        min.Y = point.Y < min.Y ? point.Y : min.Y;
        min.Z = point.Z < min.Z ? point.Z : min.Z;
        max.X = point.X > max.X ? point.X : max.X;
        max.Y = point.Y > max.Y ? point.Y : max.Y;
        max.Z = point.Z > max.Z ? point.Z : max.Z;
    }

    public Vector2 getSubPlane()
    {
        return new Vector2(Mathf.Abs(max.X - min.X), Mathf.Abs(max.Z - min.Z));
    }
}
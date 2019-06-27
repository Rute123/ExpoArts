using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    
    private Point min;
    private Point max;
    
    void Start()
    {
        min = new Point(Vector3.positiveInfinity);
        max = new Point(Vector3.negativeInfinity);
    }

    public void addPoint(Point point)
    {
        min.X = point.X < min.X ? point.X : min.X;
        min.Y = point.Y < min.Y ? point.Y : min.Y;
        min.X = point.Z < min.Z ? point.Z : min.Z;
        max.X = point.X > max.X ? point.X : max.X;
        max.Y = point.Y > max.Y ? point.Y : max.Y;
        max.Z = point.Z > max.Z ? point.Z : max.Z;
    }

    public Vector2 getSubPlane()
    {
        return new Vector2((max.X - min.X), (max.Y - min.Y));
    }
}

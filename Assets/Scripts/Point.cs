using UnityEngine;

public class Point
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Point(Vector3 inVector)
    {
        X = inVector.x;
        Y = inVector.y;
        Z = inVector.z;
    }

    public string ToString()
    {
        return new Vector3(X, Y, Z).ToString();
    }
    
}

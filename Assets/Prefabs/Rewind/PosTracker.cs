using UnityEngine;

public class PosTracker
{
    public Vector3 position;
    public Quaternion rotation;
    public int counter;
    public int travelDirection;
    public Vector3 pathDirection;
    public float cumulativeAngle;
    public float totalAngle;
    
    public PosTracker (Vector3 _position, Quaternion _rotation, int _counter, int _travelDirection, Vector3 _pathDirection, float _cumulativeAngle, float _totalAngle)
    {
        position = _position;
        rotation = _rotation;
        counter = _counter;
        travelDirection = _travelDirection;
        pathDirection = _pathDirection;
        cumulativeAngle = _cumulativeAngle;
        totalAngle = _totalAngle;
}

}

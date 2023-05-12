using UnityEngine;

public class PosTracker
{
    public Vector3 position;
    public Quaternion rotation;

    public PosTracker (Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }

}

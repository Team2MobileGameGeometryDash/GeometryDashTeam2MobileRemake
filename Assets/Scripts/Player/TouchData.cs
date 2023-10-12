using UnityEngine;

public struct TouchData
{
    public Vector2 Position;
    public Touch touch;
    public TouchData(Vector2 _position, Touch _touch)
    {
        Position = _position;
        touch = _touch;
    }


}

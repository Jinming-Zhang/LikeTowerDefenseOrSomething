using UnityEngine;

public static class CursorPosition
{
    public static Vector3 Get (float distanceFromCamera)
    { return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera)); }
}
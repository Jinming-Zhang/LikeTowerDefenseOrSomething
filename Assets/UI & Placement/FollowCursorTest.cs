using UnityEngine;

public class FollowCursorTest : MonoBehaviour
{
    public Vector3 worldCursor;
    public int cursorDepth;

    void Update()
    {
        worldCursor = Camera.main.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, cursorDepth));

        transform.position = worldCursor;
    }
}

using UnityEngine;

public class UIFollowCursor : MonoBehaviour
{

    private void Update()
    {
        transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
}

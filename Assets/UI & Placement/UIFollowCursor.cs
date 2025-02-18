using UnityEngine;

public class UIFollowCursor : MonoBehaviour
{

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}

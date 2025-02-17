using UnityEngine;

public class Flip : MonoBehaviour
{
    public void FlipTransform()
    {
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }
}

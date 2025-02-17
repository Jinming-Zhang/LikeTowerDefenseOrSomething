using UnityEngine;

public class RangeSphere : MonoBehaviour
{
    public Transform sphereToScale;
    public Vector3 defaultScale;

    public void ShowSphere(bool showSphere)
    {
        sphereToScale.gameObject.SetActive(showSphere);
    }

    public void AdjustScale(float range)
    {
        sphereToScale.localScale = defaultScale * range;
    }
}

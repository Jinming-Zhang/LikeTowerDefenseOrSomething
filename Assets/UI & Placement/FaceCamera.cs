using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Transform cameraToFollow;

    void Awake() { cameraToFollow = Camera.main.transform; }

    private void Update()
    {
        transform.LookAt(cameraToFollow);
    }
}

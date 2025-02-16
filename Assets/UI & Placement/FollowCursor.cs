using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public Transform cameraTransform;
    PlacementBox placementBox;
    
    public string targetMask;

    private void Awake()
    {
        placementBox = GetComponent<PlacementBox>();
    }


    public void Update()
    {
        RaycastHit hitInfo = new RaycastHit();

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitSomething = Physics.Raycast(camRay, out hitInfo, 100f, LayerMask.GetMask(targetMask));

        if (hitSomething)
        {
            //placementBox.ShowBox(true);
            transform.position = hitInfo.point;
        }
        else
        {
            //placementBox.ShowBox(false);
        }
    }
}

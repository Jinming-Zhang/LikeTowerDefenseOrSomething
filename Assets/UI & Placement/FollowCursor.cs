using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public Transform cameraTransform;
    PlacementBox placementBox;
    InfoDisplayPanel infoDisplayer;
    
    public string[] placementMasks;
    public string[] infoMasks;

    private void Awake()
    {
        placementBox = GetComponent<PlacementBox>();
        infoDisplayer = GetComponent<InfoDisplayPanel>();
    }


    public void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        PlacementScan(camRay, hitInfo);
        InfoScan(camRay, hitInfo);
    }

    public void PlacementScan(Ray camRay, RaycastHit hitInfo)
    {
        bool hitSomething = Physics.Raycast(camRay, out hitInfo, 100f, LayerMask.GetMask(placementMasks));
        Debug.Log("Great...");
        if (hitSomething)
        { Debug.Log("Hit still?"); transform.position = hitInfo.point; }
    }

    public void InfoScan(Ray camRay, RaycastHit hitInfo)
    {
        bool hitSomething = Physics.Raycast(camRay, out hitInfo, 100f, LayerMask.GetMask(infoMasks));

        if (hitSomething)
        {
            PlayerObjectHealth poh = hitInfo.collider.GetComponent<PlayerObjectHealth>();
            if (poh != null) { infoDisplayer.ShowPanel(poh); }
            else { infoDisplayer.HidePanel(); }
        }
        else
        {
            infoDisplayer.HidePanel();
        }
    }
}

using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public Transform cameraTransform;
    PlacementBox placementBox;
    InfoDisplayPanel infoDisplayer;
    [Space(15)]
    public string[] placementMasks;
    public string[] infoMasks;
    [Space(15)]
    [Header("Read Only")]
    public PlayerObjectHealth seenTurret;


    private void Awake()
    {
        placementBox = GetComponent<PlacementBox>();
        infoDisplayer = GetComponent<InfoDisplayPanel>();
    }


    public void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ray fakeCamRay = new Ray(Camera.main.transform.position, camRay.direction);

        Debug.DrawRay(camRay.origin, camRay.direction * 10, Color.yellow);

        PlacementScan(camRay, hitInfo);
        InfoScan(camRay, hitInfo);
    }

    public void PlacementScan(Ray camRay, RaycastHit hitInfo)
    {
        bool hitSomething = Physics.Raycast(camRay, out hitInfo, 1000f, LayerMask.GetMask(placementMasks));

        if (hitSomething) { transform.position = hitInfo.point; }
        else { Debug.Log("Hit nothing?"); }
    }

    public void InfoScan(Ray camRay, RaycastHit hitInfo)
    {
        bool hitSomething = Physics.Raycast(camRay, out hitInfo, 1000f, LayerMask.GetMask(infoMasks));

        if (hitSomething && !hoverObstructed)
        {
            seenTurret = hitInfo.collider.GetComponent<PlayerObjectHealth>();
            TowerShoot tower = seenTurret.GetComponent<TowerShoot>();
            Battery bat = seenTurret.GetComponent<Battery>();

            if (seenTurret != null)
            {
                if(tower != null) { infoDisplayer.ShowPanel(seenTurret, tower); }
                else if (bat != null) { infoDisplayer.ShowPanel(seenTurret, bat); }
                else { infoDisplayer.ShowPanel(seenTurret); }

            }
            else { infoDisplayer.HidePanel(); }
        }
        else
        {
            infoDisplayer.HidePanel();
            seenTurret = null;
        }
    }

    public bool hoverObstructed = false;
    public void ObstructHover(bool obstructed)
    { hoverObstructed = obstructed; }
}

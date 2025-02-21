using UnityEngine;

public class TurretPlacer: MonoBehaviour
{
    // This script says "turret" but it can work with any GameObject we assign it to.

    public bool dropTurretOnPlace;
    public bool deleteMode = false;

    PlacementBox placementBox;
    [SerializeField] RangeSphere energyRangeSphere;
    [SerializeField] RangeSphere threatRangeSphere;
    FollowCursor followCursor;
    [Space(15)]
    public GameObject[] deleteIcons;
    public GameObject placementMessage;
    public GameObject deletionMessage;

    [Space(15)]
    [Header("Read Only")]
    public GameObject heldTurret;

    private void Awake()
    {
        placementBox = GetComponent<PlacementBox>();
        followCursor = GetComponent<FollowCursor>();
    }

    public void Update()
    {
        if(hoverObstructed) { return; }

        if(Input.GetButtonDown("Fire1"))
        {
            if (deleteMode) { TryDeleteTurret(); }
            if (heldTurret != null) { PlaceTurret(); }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (deleteMode) { EnableDeleteMode(false); }
            if (heldTurret != null) { DeselectTurret(); }
        }
    }

    public void PlaceTurret()
    {
        if (!placementBox.canPlace) { Debug.LogWarning("Can't place there."); return; }

        Instantiate(heldTurret, transform.position, transform.rotation);
        if(dropTurretOnPlace) { DeselectTurret(); }
    }

    public void SelectTurret(GameObject turret)
    {
        heldTurret = turret;
        placementBox.ShowBox(true);

        BoxCollider collider = turret.GetComponent<BoxCollider>();

        if(collider == null) { Debug.LogError("Object needs a collider to scale the PlacementBox!"); }
        else { placementBox.RescaleBox(new Vector3(collider.size.x, collider.size.y, collider.size.z)); }


        TowerShoot isATurret = turret.GetComponent<TowerShoot>();
        if (isATurret != null)
        {
            threatRangeSphere.AdjustScale(isATurret.range); threatRangeSphere.ShowSphere(true);
            energyRangeSphere.AdjustScale(isATurret.batteryRange); energyRangeSphere.ShowSphere(true);
        }

        Battery isABattery = turret.GetComponent<Battery>();
        if (isABattery != null)
        {
            energyRangeSphere.AdjustScale(isABattery.range); energyRangeSphere.ShowSphere(true);
            threatRangeSphere.ShowSphere(false);
        }

        EnableDeleteMode(false);
        //if (placementMessage == null) { Debug.LogWarning("No delete mode message detected."); }
        //else { placementMessage.SetActive(true); }
    }

    public void DeselectTurret()
    {
        heldTurret = null;
        placementBox.ShowBox(false);
        threatRangeSphere.ShowSphere(false);
        energyRangeSphere.ShowSphere(false);

        //if(placementMessage == null) { Debug.LogWarning("No delete mode message detected."); }
        //else { placementMessage.SetActive(false); }
    }

    public void EnableDeleteMode(bool enterDeleteMode)
    {
        deleteMode = enterDeleteMode;
        if (deleteMode)
        {
            ToggleDeleteIcon(true);
            DeselectTurret();
        }
        else
        { ToggleDeleteIcon(false); }
    }

    public void ToggleDeleteIcon(bool isActive)
    {
        /*if(deleteIcon == null)
        {
            Debug.LogError("[" + gameObject.name + "]: Please drag the Delete Icon under UI Cursor Follower into its respective slot (under \"TurretPlacer\") in the inspector.");
        }
        else { deleteIcon.SetActive(isActive); }*/

        foreach(GameObject go in deleteIcons)
        {
            go.SetActive(isActive);
        }

        //if (deletionMessage == null)
        //{ Debug.LogWarning("No delete mode message detected."); }
        //else { deletionMessage.SetActive(isActive); }
    }

    public void TryDeleteTurret()
    {
        if(followCursor.seenTurret != null)
        { Destroy(followCursor.seenTurret.gameObject); }
    }

    public bool hoverObstructed = false;
    public void ObstructHover(bool obstructed)
    { hoverObstructed = obstructed; }
}

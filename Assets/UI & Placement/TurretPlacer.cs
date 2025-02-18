using UnityEngine;

public class TurretPlacer: MonoBehaviour
{
    // This script says "turret" but it can work with any GameObject we assign it to.

    public bool dropTurretOnPlace;
    public bool deleteMode = false;

    PlacementBox placementBox;
    RangeSphere rangeSphere;
    FollowCursor followCursor;

    [Header("Read Only")]
    public GameObject heldTurret;

    private void Awake()
    {
        placementBox = GetComponent<PlacementBox>();
        rangeSphere = GetComponent<RangeSphere>();
        followCursor = GetComponent<FollowCursor>();
    }

    public void Update()
    {
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
        { rangeSphere.AdjustScale(isATurret.range); rangeSphere.ShowSphere(true); }

        Battery isABattery = turret.GetComponent<Battery>();
        if (isABattery != null)
        { rangeSphere.AdjustScale(isABattery.range); rangeSphere.ShowSphere(true); }

    }

    public void DeselectTurret()
    {
        heldTurret = null;
        placementBox.ShowBox(false);
        rangeSphere.ShowSphere(false);
    }

    public void EnableDeleteMode(bool enterDeleteMode)
    {
        deleteMode = enterDeleteMode;
        if (deleteMode) { DeselectTurret(); }
    }
    public void TryDeleteTurret()
    {
        if(followCursor.seenTurret != null)
        { Destroy(followCursor.seenTurret.gameObject); }
    }
}

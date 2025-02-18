using UnityEngine;

public class TurretPlacer: MonoBehaviour
{
    // This script says "turret" but it can work with any GameObject we assign it to.

    [Header("Read Only")]
    public GameObject heldTurret;
    public bool dropTurretOnPlace;

    PlacementBox placementBox;
    RangeSphere rangeSphere;

    private void Awake() { placementBox = GetComponent<PlacementBox>(); rangeSphere = GetComponent<RangeSphere>(); }

    public void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            PlaceTurret();
        }
    }

    public void PlaceTurret()
    {
        if (heldTurret == null) { return; }
        else if (!placementBox.canPlace) { Debug.LogWarning("Can't place there."); return; }

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
}

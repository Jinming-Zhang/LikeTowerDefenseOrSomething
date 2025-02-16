using UnityEngine;

public class TurretPlacer: MonoBehaviour
{
    // This script says "turret" but it can work with any GameObject we assign it to.

    [Header("Read Only")]
    public GameObject heldTurret;
    public bool dropTurretOnPlace;

    PlacementBox placementBox;

    private void Awake() { placementBox = GetComponent<PlacementBox>(); }

    public void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            PlaceTurret();
        }
    }

    public void PlaceTurret()
    {
        if(!placementBox.canPlace) { Debug.LogWarning("Can't place there."); return; }
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

    }

    public void DeselectTurret()
    {
        heldTurret = null;
        placementBox.ShowBox(false);
    }
}

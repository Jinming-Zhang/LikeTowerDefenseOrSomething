using UnityEngine;

public class TurretSelectBox : MonoBehaviour
{
    public GameObject prefabReference;

    public void SelectTurret()
    {
        TurretPlacer tp = FindFirstObjectByType<TurretPlacer>();
        tp.SelectTurret(prefabReference);
    }
}

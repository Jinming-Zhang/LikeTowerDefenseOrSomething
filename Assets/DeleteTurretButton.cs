using UnityEngine;

public class DeleteTurretButton : MonoBehaviour
{
    public void EnterDeleteMode(bool b)
    {
        TurretPlacer tp = FindFirstObjectByType<TurretPlacer>();
        if(tp == null) { Debug.LogError( gameObject.name + ": No TurretPlacer in scene!?"); }
        else
        { tp.EnableDeleteMode(b); }
    }
}

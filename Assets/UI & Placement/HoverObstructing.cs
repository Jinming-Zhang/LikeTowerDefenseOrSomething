using UnityEngine;
using UnityEngine.EventSystems;

public class HoverObstructing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    FollowCursor followCursor;
    TurretPlacer turretPlacer;

    public void Awake()
    { turretPlacer = FindFirstObjectByType<TurretPlacer>(); followCursor = FindFirstObjectByType<FollowCursor>(); }

    public void OnPointerEnter(PointerEventData pointerEventData)
    { turretPlacer.ObstructHover(true); followCursor.ObstructHover(true); }

    public void OnPointerExit(PointerEventData pointerEventData)
    { turretPlacer.ObstructHover(false); followCursor.ObstructHover(false); }
}

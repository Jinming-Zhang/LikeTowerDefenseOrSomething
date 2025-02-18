using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretSelectBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Note: Space between panels is 78 units.

    public GameObject prefabReference;
    public TMP_Text costText;
    [Space(20)]
    public GameObject infoDisplay;
    public TMP_Text nameText;
    public TMP_Text hpText;
    [Space(15)]
    public TMP_Text dmgText;
    public TMP_Text reloadText;
    public TMP_Text rangeText;
    public TMP_Text pwrText;


    private void Start()
    {
        #region Setting infoDisplay values
        PlayerObjectHealth poh = prefabReference.GetComponent<PlayerObjectHealth>();
        if(poh != null)
        {
            nameText.text = poh.displayName;
            hpText.text = poh.health.ToString() + " / " + poh.health.ToString();
            costText.text = poh.metalCost.ToString();
        }

        TowerShoot twr = prefabReference.GetComponent<TowerShoot>();
        if(twr != null)
        {
            dmgText.text = twr.damage.ToString("00");
            reloadText.text = twr.reloadTime.ToString("00.0") + "s";
            rangeText.text = twr.range.ToString("000") + "m";
            pwrText.text = twr.batteryCapacity.ToString("00");
        }

        Battery bat = prefabReference.GetComponent<Battery>();
        if (bat != null)
        {
            dmgText.text = "--";
            reloadText.text = "-.--" + "s";
            rangeText.text = bat.range.ToString("---") + "m";
            pwrText.text = bat.capacity.ToString("00");
        }
        #endregion
        
        // Ignore this one
        #region Setting Up Button
        /*Button attachedButton = GetComponent<Button>();

        if(attachedButton == null) { Debug.LogError(gameObject.name + " needs a button!"); }
        else
        { attachedButton.onClick.AddListener(SelectTurret); }*/
        #endregion
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    { infoDisplay.SetActive(true); }

    public void OnPointerExit(PointerEventData pointerEventData)
    { infoDisplay.SetActive(false); }

    public void SelectTurret()
    {
        TurretPlacer tp = FindFirstObjectByType<TurretPlacer>();
        if (tp == null) { Debug.LogError(gameObject.name + ": No TurretPlacer in scene!?"); }
        else { tp.SelectTurret(prefabReference); }
    }
}

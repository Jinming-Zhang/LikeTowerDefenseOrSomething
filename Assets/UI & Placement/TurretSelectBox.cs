using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretSelectBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
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
            dmgText.text = twr.damage.ToString("--");
            reloadText.text = twr.reloadTime.ToString("-.--") + "s";
            rangeText.text = twr.range.ToString("---") + "m";
            pwrText.text = bat.capacity.ToString("00");
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        infoDisplay.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        infoDisplay.SetActive(false);
    }

    public void SelectTurret()
    {
        TurretPlacer tp = FindFirstObjectByType<TurretPlacer>();
        tp.SelectTurret(prefabReference);
    }
}

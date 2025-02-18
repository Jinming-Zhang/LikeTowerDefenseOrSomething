using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurretSelectBox : MonoBehaviour
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
            hpText.text = poh.health.ToString("000") + " / " + poh.health.ToString("000");
        }

        TowerShoot twr = prefabReference.GetComponent<TowerShoot>();
        if(twr != null)
        {
            dmgText.text = twr.damage.ToString("00");
            reloadText.text = twr.reloadTime.ToString("00") + "s";
            rangeText.text = twr.range.ToString("000");
        }

        Battery isABattery = prefabReference.GetComponent<Battery>();

        if (isABattery != null) {; }
    }

    public void OnMouseEnter()
    {
        infoDisplay.SetActive(false);
    }

    public void OnMouseExit()
    {
        infoDisplay.SetActive(true);
    }

    public void SelectTurret()
    {
        TurretPlacer tp = FindFirstObjectByType<TurretPlacer>();
        tp.SelectTurret(prefabReference);
    }
}

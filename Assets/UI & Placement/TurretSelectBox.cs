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
    public TMP_Text dmgText;
    public TMP_Text hpText;
    public TMP_Text reloadText;
    public TMP_Text pwrText;


    private void Start()
    {
        Turret isATurret = prefabReference.GetComponent<Turret>();

        if(isATurret != null)
        {
            nameText.text = isATurret.displayName;
            dmgText.text = 0; //Ask about damage... 
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

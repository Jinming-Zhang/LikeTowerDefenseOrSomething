using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDisplayPanel : MonoBehaviour
{
    public GameObject panelObject;
    public TMP_Text nameText;
    [Space(15)]
    public RectTransform hpBar;
    public TMP_Text hpText;
    [Space(15)]
    public RectTransform pwrBar;
    public TMP_Text pwrText;
    public RectTransform pwrOverfillBar;
    [Space(15)]
    [Header("Read Only")]
    public PlayerObjectHealth referencePOH;
    public TowerShoot referenceTower;
    public Battery referenceBattery;

    public void ShowPanel(PlayerObjectHealth poh)
    {
        ResetReferences();

        if (!panelObject.activeSelf) { panelObject.SetActive(true); }
        nameText.text = poh.displayName;
    }

    public void ShowPanel(PlayerObjectHealth poh, TowerShoot ts)
    {
        ResetReferences();

        if (!panelObject.activeSelf) { panelObject.SetActive(true); }
        nameText.text = poh.displayName;

        referencePOH = poh;
        referenceTower = ts;
    }

    public void ShowPanel(PlayerObjectHealth poh, Battery b)
    {
        ResetReferences();

        if (!panelObject.activeSelf) { panelObject.SetActive(true); }
        nameText.text = poh.displayName;

        referencePOH = poh;
        referenceBattery = b;
    }

    private void Update()
    {
        if (referencePOH != null)
        {
            hpBar.localScale = new Vector3(referencePOH.health / referencePOH.maxHealth, hpBar.localScale.y, hpBar.localScale.z);
            hpText.text = referencePOH.health.ToString("00") + " / " + referencePOH.maxHealth.ToString("00");
        }

        if(referenceTower != null)
        {
            if(referenceTower.NearbyBattery != null)
            {
                pwrBar.transform.localScale = new Vector3(1f, hpBar.localScale.y, hpBar.localScale.z);
                pwrText.text = "POWERED";
            }
            else
            {
                pwrBar.transform.localScale = new Vector3(0f, hpBar.localScale.y, hpBar.localScale.z);
                pwrText.text = "UNPOWERED";
            }
        }

        if(referenceBattery != null)
        {
            pwrText.text = referenceBattery.used.ToString("00") + " / " + referenceBattery.capacity.ToString("00");

            if(referenceBattery.used > referenceBattery.capacity)
            {
                pwrBar.transform.localScale = new Vector3(1f, hpBar.localScale.y, hpBar.localScale.z);
                pwrOverfillBar.transform.localScale =
                    new Vector3((referenceBattery.used - referenceBattery.capacity) / referenceBattery.capacity, hpBar.localScale.y, hpBar.localScale.z);
            }
            else
            {
                pwrBar.transform.localScale = new Vector3(referenceBattery.used / referenceBattery.capacity, hpBar.localScale.y, hpBar.localScale.z);
                pwrOverfillBar.transform.localScale = new Vector3(0f, hpBar.localScale.y, hpBar.localScale.z);
            }

        }
    }

    public void HidePanel()
    {
        if(panelObject.activeSelf) { panelObject.SetActive(false); ResetReferences(); }
    }

    public void ResetReferences()
    {
        referencePOH = null;
        referenceTower = null;
        referenceBattery = null;
    }
}

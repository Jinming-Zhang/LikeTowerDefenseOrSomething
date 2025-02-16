using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDisplayPanel : MonoBehaviour
{
    public GameObject panelObject;
    public TMP_Text nameText;
    public RectTransform hpBar;
    public TMP_Text hpText;


    public void ShowPanel(PlayerObjectHealth poh)
    {
        if (!panelObject.activeSelf) { panelObject.SetActive(true); }

        nameText.text = poh.displayName;
        hpBar.localScale = new Vector3(poh.health / poh.maxHealth, hpBar.localScale.y, hpBar.localScale.z);
    }

    public void HidePanel()
    {
        if(panelObject.activeSelf) { panelObject.SetActive(false); }
    }
}

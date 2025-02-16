using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDisplayPanel : MonoBehaviour
{
    public GameObject panelObject;
    public TMP_Text nameText;
    public RectTransform hpBar;
    public TMP_Text hpText;

    public void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    public void HidePanel()
    {
        panelObject.SetActive(false);
    }
}

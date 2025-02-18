using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] thingsToDisplay;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        foreach(GameObject go in thingsToDisplay)
        { go.SetActive(true); }

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        foreach (GameObject go in thingsToDisplay)
        { go.SetActive(false); }
    }
}

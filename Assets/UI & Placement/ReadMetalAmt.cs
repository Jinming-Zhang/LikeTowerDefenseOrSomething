using UnityEngine;
using TMPro;

public class ReadMetalAmt : MonoBehaviour
{
    Resources resourceManager;
    TMP_Text display;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        resourceManager = FindFirstObjectByType<Resources>();
        display = GetComponent<TMP_Text>();

        if(resourceManager == null)
        { Debug.LogError("[" + gameObject.name + "]: Couldn't find a Resources script to read!"); }
    }

    void Update()
    {
        if (resourceManager != null)
        {
            display.text = resourceManager.amount.ToString();
        }
    }
}

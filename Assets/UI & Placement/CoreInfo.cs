using UnityEngine;

public class CoreReader : InfoDisplayPanel
{
    [Space(20)]
    [Header("Core Reader")]
    public PlayerObjectHealth coreHP;

    private void Awake()
    {
        referencePOH = coreHP;
    }

    private void Start()
    {
        nameText.text = referencePOH.displayName;
    }
}

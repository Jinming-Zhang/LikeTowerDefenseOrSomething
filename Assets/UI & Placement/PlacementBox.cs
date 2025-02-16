using UnityEngine;

public class PlacementBox : MonoBehaviour
{
    public string[] obstructionMasks;
    [Space(20)]
    public Transform positionReference;
    public GameObject masterBox;
    public GameObject oKBox;
    public GameObject notOKBox;

    public bool canPlace { get { return oKBox.activeSelf && masterBox.activeSelf; } }

    // Our placement box forecasts where the player's turret (or w/e) will be placed.
    // We can adjust the scale of the "Placement Marker", the top object in the hierarchy,
    // according to the size of the turret and use that for the reference of our OverlapBox below.
    //
    // We use the position of the "Display Box" object because its center will modulate
    // with the parent's scale. Thus, the turret's base will always be on the ground.
    //
    // Any game object that would block placement should have its mask under obstructionMasks.
    // We display a green "oKBox" when placement is allowed and a red "notOKBox" when it isn't.

    public void Update()
    {
        if(Physics.OverlapBox(positionReference.position, transform.localScale * 0.5f, transform.rotation, LayerMask.GetMask(obstructionMasks)).Length <= 0)
        { if (!oKBox.activeSelf) { oKBox.SetActive(true); notOKBox.SetActive(false); } }
        else
        { if (oKBox.activeSelf) { oKBox.SetActive(false); notOKBox.SetActive(true); } }
    }

    public void ShowBox(bool shouldNowBeActive)
    {
        if (masterBox.activeSelf != shouldNowBeActive)
        { masterBox.SetActive(shouldNowBeActive); }
    }

    public void RescaleBox(Vector3 newScale) { transform.localScale = newScale; }
}

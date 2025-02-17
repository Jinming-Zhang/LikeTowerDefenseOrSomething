using UnityEngine;

public class SlidingUI : MonoBehaviour
{
    public int currentPosition = 0;
    public Vector3[] positions;

    private void Start()
    { SetPosition(currentPosition); }

    public void SetPosition(int positionIndex)
    {
        if(positionIndex < 0 || positionIndex >= positions.Length) { Debug.LogError("Bad position index for sliding UI!"); } 
        transform.localPosition = positions[positionIndex];
        currentPosition = positionIndex;
    }

    public void TogglePosition()
    {
        currentPosition++; if (currentPosition >= positions.Length) { currentPosition = 0; }
        SetPosition(currentPosition);
    }
}

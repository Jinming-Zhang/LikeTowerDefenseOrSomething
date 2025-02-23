using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string mainSceneName = "Game";

    public void SwitchScene()
    { SceneManager.LoadScene(mainSceneName); }
}

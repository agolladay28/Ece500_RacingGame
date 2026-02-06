using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene to load on click")]
    [Tooltip("Enter the exact name of the scene to switch to. Scene must be in Build Settings.")]
    public string sceneName;

    // This function can be hooked to UI Buttons
    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("Scene name is empty! Please assign a scene in the Inspector.");
            return;
        }

        Debug.Log("Switching to scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}

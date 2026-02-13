using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene to load on click")]
    [Tooltip("Enter the exact name of the scene to switch to. Scene must be in Build Settings.")]
    public string sceneName;

    // This function can be hooked to UI Buttons
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string current = SceneManager.GetActiveScene().name;

            if (current == "Start Page")
                SceneManager.LoadScene("Control");

            else if (current == "Control")
                SceneManager.LoadScene("Track");

            else if (current == "Track Selection")
                SceneManager.LoadScene("Track");
        }
    }
}

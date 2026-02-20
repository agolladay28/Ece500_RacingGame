using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    void Update()
    {
        string current = SceneManager.GetActiveScene().name;

        // Enter key navigation
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (current == "Start Page")
                SceneManager.LoadScene("Control");
            else if (current == "Control")
                SceneManager.LoadScene("Track Selection");
        }

        // Track selection logic - ONLY runs if we are on the Track Selection screen
        if (current == "Track Selection")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                SceneManager.LoadScene("oval_track");

            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                SceneManager.LoadScene("oval_track1");

            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                SceneManager.LoadScene("oval_track2");

            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                SceneManager.LoadScene("oval_track3");
        }
    }
}

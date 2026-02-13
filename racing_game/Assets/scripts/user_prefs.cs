using UnityEngine;

public class user_prefs : MonoBehaviour
{
    private const string fullscreen_pref_str = "fullscreen_pref_str";

    private int windowed_height = 720;
    private int windowed_width = 1280;
    private const string windowed_height_pref_str = "windowed_height_pref_str";
    private const string windowed_width_pref_str = "windowed_width_pref_str";


    void Start()
    {

        bool fullscreen = PlayerPrefs.GetInt(fullscreen_pref_str) == 1;
        set_fullscreen(fullscreen);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            toggle_fullscreen();
        }
        if (!Screen.fullScreen)
        {
            if (Screen.height != windowed_height)
            {
                windowed_height = Screen.height;
                set_resolution_pref(windowed_height_pref_str, windowed_height);
            }
            if (Screen.width != windowed_width)
            {
                windowed_width = Screen.width;
                set_resolution_pref(windowed_width_pref_str, windowed_width);
            }
        }
    }
    private void set_resolution_pref(string key, int res)
    {
        PlayerPrefs.SetInt(key, res);
        PlayerPrefs.Save();
    }
    public void toggle_fullscreen()
    {
        set_fullscreen(!Screen.fullScreen);
    }
    public void set_fullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        if (fullscreen)
        {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);

        }
        else
        {
            int width = PlayerPrefs.GetInt(windowed_width_pref_str, windowed_width);
            int height = PlayerPrefs.GetInt(windowed_height_pref_str, windowed_height);
            Screen.SetResolution(width, height, false);
        }
        int tmp = fullscreen ? 1 : 0;
        PlayerPrefs.SetInt(fullscreen_pref_str, tmp);
        PlayerPrefs.Save();
    }
}

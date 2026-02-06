using System;
using UnityEngine;
using UnityEngine.UIElements;

public class two_player_race_info : MonoBehaviour
{
    public UIDocument document;
    //max speed is what is shown when needle is at max
    public float total_laps = 3;
    public float left_current_lap = 1;
    public float right_current_lap = 1;
    public float left_current_lap_time_sec = 0f;
    public float right_current_lap_time_sec = 0f;

    Label left_lap_text;
    Label left_time_text;
    Label right_lap_text;
    Label right_time_text;

    void OnEnable()
    {

        var doc_root = document.rootVisualElement;
        var flex_root = doc_root.Q<VisualElement>("root");
        var left_root = flex_root.Q<VisualElement>("left_player");
        var right_root = flex_root.Q<VisualElement>("right_player");
        left_lap_text = left_root.Q<Label>("lap_count");
        left_time_text = left_root.Q<Label>("lap_time");
        right_lap_text = right_root.Q<Label>("lap_count");
        right_time_text = right_root.Q<Label>("lap_time");
    }


    // Update is called once per frame
    void Update()
    {
        update_left();
        update_right();
    }
    private void update_left()
    {
        left_lap_text.text = $"Lap: {left_current_lap}/{total_laps}";
        left_time_text.text = time_to_string(left_current_lap_time_sec);
    }
    private void update_right()
    {
        right_lap_text.text = $"Lap: {right_current_lap}/{total_laps}";
        right_time_text.text = time_to_string(right_current_lap_time_sec);
    }
    private string time_to_string(float time_seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(time_seconds);

        string timeStr = string.Format("{0:D2}:{1:D2}:{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);
        return timeStr;

    }
}

using System;
using UnityEngine;
using UnityEngine.UIElements;

public class race_info : MonoBehaviour
{
    public UIDocument document;
    //max speed is what is shown when needle is at max
    public float total_laps = 3;
    public float current_lap = 1;
    public float current_lap_time_sec = 59.988f;
    [SerializeField] private string player_visual_element_query = "left_player";
    Label lap_text;
    Label time_text;

    void OnEnable()
    {

        var doc_root = document.rootVisualElement;
        var player_root = doc_root.Q<VisualElement>(player_visual_element_query);
        lap_text = player_root.Q<Label>("lap_count");
        time_text = player_root.Q<Label>("lap_time");
    }


    // Update is called once per frame
    void Update()
    {
        lap_text.text = $"Lap: {current_lap}/{total_laps}";
        time_text.text = time_to_string(current_lap_time_sec);
    }
    private string time_to_string(float time_seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(time_seconds);

        string timeStr = string.Format("{0:D2}:{1:D2}:{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);
        return timeStr;

    }
}

using System;
using System.Collections.Generic;
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
    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    [SerializeField] private bool debug_lap_trigger = false;
    private List<long> lap_times = new List<long>();
    private float final_time_sec = 0;
    void OnEnable()
    {

        var doc_root = document.rootVisualElement;
        var player_root = doc_root.Q<VisualElement>(player_visual_element_query);
        lap_text = player_root.Q<Label>("lap_count");
        time_text = player_root.Q<Label>("lap_time");
    }
    void Start()
    {
        start_race();
    }

    // Update is called once per frame
    void Update()
    {
        if (debug_lap_trigger)
        {
            finish_lap();
            debug_lap_trigger = false;
        }
        if (current_lap != total_laps)
        {
            current_lap_time_sec = stopwatch.ElapsedMilliseconds / 1000f;
            lap_text.text = $"Lap: {current_lap}/{total_laps}";
            time_text.text = time_to_string(current_lap_time_sec);
        }
        if (current_lap == total_laps)
        {
            stopwatch.Stop();
            finish_race();
            time_text.text = time_to_string(final_time_sec);
        }
    }
    private void finish_race()
    {
        final_time_sec = 0;
        foreach (long curr in lap_times)
        {
            final_time_sec += curr;
        }
        return;
    }
    private void start_race()
    {
        current_lap = 1;
        current_lap_time_sec = 0;
        stopwatch.Start();
    }
    private void finish_lap()
    {
        current_lap++;
        lap_times.Add(stopwatch.ElapsedMilliseconds);
        stopwatch.Restart();

    }
    private string time_to_string(float time_seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(time_seconds);

        string timeStr = string.Format("{0:D2}:{1:D2}:{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);
        return timeStr;

    }
}

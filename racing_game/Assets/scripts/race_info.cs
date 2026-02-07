using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class race_info : MonoBehaviour
{
    public UIDocument document;
    //max speed is what is shown when needle is at max
    public float total_laps = 1;
    public float current_lap = 1;
    public int last_checkpoint = 0;
    public float current_lap_time_sec = 59.988f;
    [SerializeField] private string player_visual_element_query = "left_player";
    Label lap_text;
    Label time_text;
    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    public bool lap_trigger = false;
    public List<long> lap_times = new List<long>();
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
        if (Time.timeScale == 0)
        {
            return;
        }
        current_lap_time_sec = stopwatch.ElapsedMilliseconds / 1000f;
        lap_text.text = $"Lap: {current_lap}/{total_laps}";
        time_text.text = time_to_string(current_lap_time_sec);
        if (lap_trigger)
        {
            finish_lap();
            lap_trigger = false;
        }
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
    public string get_total_time_string()
    {
        float total = 0;
        foreach (long time in lap_times)
        {
            total += time / 1000.0f;
        }
        return time_to_string(total);
    }
    private string time_to_string(float time_seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(time_seconds);

        string timeStr = string.Format("{0:D2}:{1:D2}:{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);
        return timeStr;

    }
}

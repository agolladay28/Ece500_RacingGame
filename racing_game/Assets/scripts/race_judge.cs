using System;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class race_judge : MonoBehaviour
{
    public race_info left_car_info;
    public race_info right_car_info;
    public race_setup race_setup;
    public int left_car_checkpoint = 0;
    public int right_car_checkpoint = 0;
    public int checkpoints_per_lap;
    private bool is_winner_declared = false;
    private race_info winner_info;

    public UIDocument winner_ui;
    private VisualElement doc_root;
    private Label winner_name_label;
    private Label winner_time_label;


    void OnEnable()
    {
        doc_root = winner_ui.rootVisualElement;
        doc_root.style.display = DisplayStyle.None;
        var winner_ui_root = doc_root.Q<VisualElement>("winner_root_div");
        winner_name_label = winner_ui_root.Q<Label>("winner_name_text");
        winner_time_label = winner_ui_root.Q<Label>("winner_time_text");

    }
    void Update()
    {
        if (left_car_checkpoint != 0)
        {
            update_car(left_car_info, left_car_checkpoint);
            left_car_checkpoint = 0;
        }
        if (right_car_checkpoint != 0)
        {
            update_car(right_car_info, right_car_checkpoint);
            right_car_checkpoint = 0;
        }
        if (is_winner_declared)
        {
            Time.timeScale = 0;
            declare_winner();
            if (Input.GetKey(KeyCode.R))
            {
                reset_scene();
            }
        }
    }
    private void reset_scene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    private void update_car(race_info info, int checkpoint_number)
    {
        //do nothing if its the wrong checkpoint
        if (info.last_checkpoint + 1 != checkpoint_number)
        {
            Debug.Log($"Wanted: {info.last_checkpoint + 1}, Got: {checkpoint_number}");
            return;
        }
        info.last_checkpoint = checkpoint_number;
        if (info.last_checkpoint == checkpoints_per_lap)
        {
            info.lap_trigger = true;
            info.last_checkpoint = 0;
        }
        if (!is_winner_declared && info.current_lap == race_setup.total_laps + 1)
        {
            winner_info = info;
            is_winner_declared = true;
        }

    }
    private void declare_winner()
    {
        winner_time_label.text = "Time: " + winner_info.get_total_time_string();
        winner_name_label.text = winner_info.car_color + " Won!";
        doc_root.style.display = DisplayStyle.Flex;
    }

}

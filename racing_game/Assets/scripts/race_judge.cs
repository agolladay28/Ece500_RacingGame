using System;
using NUnit.Framework.Interfaces;
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
    public pause_menu pause_menu;
    public audio_manager audio_manager;


    private bool winner_announced = false;
    private bool is_winner_declared = false;
    private race_info winner_info;


    public UIDocument winner_ui;
    private VisualElement winner_doc_root;
    private Label winner_name_label;
    private Label winner_time_label;

    public UIDocument HUD_ui;
    private VisualElement start_lights;
    private VisualElement light1;
    private VisualElement light2;
    private VisualElement light3;
    private bool is_counting_down = true;
    private float countdown_time = 0;
    bool countdown_flag1, countdown_flag2, coundown_flag3, countdown_flag4;


    void OnEnable()
    {


        winner_doc_root = winner_ui.rootVisualElement;
        winner_doc_root.style.display = DisplayStyle.None;
        var winner_ui_root = winner_doc_root.Q<VisualElement>("winner_root_div");
        winner_name_label = winner_ui_root.Q<Label>("winner_name_text");
        winner_time_label = winner_ui_root.Q<Label>("winner_time_text");

        var HUD_root = HUD_ui.rootVisualElement;
        var HUD_root_div = HUD_root.Q<VisualElement>("root");
        start_lights = HUD_root_div.Q<VisualElement>("start_lights");
        light1 = start_lights.Q<VisualElement>("light1");
        light2 = start_lights.Q<VisualElement>("light2");
        light3 = start_lights.Q<VisualElement>("light3");

    }
    void Awake()
    {
        countdown_flag1 = countdown_flag2 = coundown_flag3 = countdown_flag4 = true;
    }
    void Update()
    {
        if (is_counting_down)
        {
            countdown();
            return;
        }
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
            if (Input.GetKey(KeyCode.Return))
            {
                pause_menu.restart_race();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause_menu.request_pause();
        }

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
        if (winner_announced)
        {
            return;
        }
        pause_menu.pause_cars();
        winner_time_label.text = "Time: " + winner_info.get_total_time_string();
        winner_name_label.text = winner_info.car_color + " Won!";
        winner_doc_root.style.display = DisplayStyle.Flex;
        audio_manager.announce_winner(winner_info.car_color);
        winner_announced = true;
    }
    private void countdown()
    {

        countdown_time += Math.Min(Time.unscaledDeltaTime, 0.1f);
        if (countdown_time < 4)
        {
            //disable the cars until after the 3-2-1 start countdown
            Time.timeScale = 0;
            pause_menu.pause_cars();
            left_car_info.reset_time();
            right_car_info.reset_time();
        }
        if (countdown_time >= 1 && countdown_flag1)
        {
            make_light_red(light1);
            audio_manager.countdown_beep();
            countdown_flag1 = false;
        }
        if (countdown_time >= 2 && countdown_flag2)
        {
            make_light_red(light2);
            audio_manager.countdown_beep();
            countdown_flag2 = false;
        }
        if (countdown_time >= 3 && coundown_flag3)
        {
            make_light_red(light3);
            audio_manager.countdown_beep();
            coundown_flag3 = false;
        }
        if (countdown_time >= 4 && countdown_flag4)
        {
            make_light_green(light1);
            make_light_green(light2);
            make_light_green(light3);
            audio_manager.go_beep();

            pause_menu.resume_cars();
            Time.timeScale = 1;

            countdown_flag4 = false;
        }
        if (countdown_time >= 5)
        {
            start_lights.style.display = DisplayStyle.None;
            is_counting_down = false;//ensures countdown() not called again

        }

    }
    private void make_light_green(VisualElement light)
    {
        var img = light as Image;
        img.tintColor = Color.green;
    }
    private void make_light_red(VisualElement light)
    {
        var img = light as Image;
        img.tintColor = Color.red;
    }
}

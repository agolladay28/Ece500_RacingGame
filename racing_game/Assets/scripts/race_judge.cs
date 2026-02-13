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
    public AudioSource left_car_engine;
    public AudioSource left_car_tire;
    public AudioSource right_car_engine;
    public AudioSource right_car_tire;
    public AudioSource announcer;
    public AudioClip left_wins;
    public AudioClip right_wins;
    private bool winner_announced = false;
    private bool is_winner_declared = false;
    private race_info winner_info;
    private bool is_pause_requested = false;
    private bool is_paused = false;

    public UIDocument pause_ui;
    private VisualElement pause_doc_root;

    public UIDocument winner_ui;
    private VisualElement winner_doc_root;
    private Label winner_name_label;
    private Label winner_time_label;


    void OnEnable()
    {
        pause_doc_root = pause_ui.rootVisualElement;
        pause_doc_root.style.display = DisplayStyle.None;

        winner_doc_root = winner_ui.rootVisualElement;
        winner_doc_root.style.display = DisplayStyle.None;
        var winner_ui_root = winner_doc_root.Q<VisualElement>("winner_root_div");
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
            if (Input.GetKey(KeyCode.Return))
            {
                return_to_main_menu();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            request_pause();
        }

    }
    private void request_pause()
    {
        if (is_paused)
        {
            resume();
        }
        else
        {
            pause();
        }
    }
    private void resume()
    {
        resume_car_audio();
        is_paused = false;
        Time.timeScale = 1;
        pause_doc_root.style.display = DisplayStyle.None;
    }
    private void pause()
    {
        is_paused = true;
        mute_car_audio();
        Time.timeScale = 0;
        pause_doc_root.style.display = DisplayStyle.Flex;
    }
    private void return_to_main_menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Page");

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
        mute_car_audio();
        winner_time_label.text = "Time: " + winner_info.get_total_time_string();
        winner_name_label.text = winner_info.car_color + " Won!";
        winner_doc_root.style.display = DisplayStyle.Flex;
        announce_winner();
    }
    private void announce_winner()
    {
        if (winner_announced)
        {
            return;
        }
        if (winner_info.car_color == "Yellow")
        {
            announcer.PlayOneShot(right_wins, 1.2f);
        }
        else
        {
            announcer.PlayOneShot(left_wins, 1.2f);
        }
        winner_announced = true;
    }
    private void resume_car_audio()
    {

        left_car_engine.Play();
        left_car_tire.Play();
        right_car_engine.Play();
        right_car_tire.Play();
    }
    private void mute_car_audio()
    {
        left_car_engine.Stop();
        left_car_tire.Stop();
        right_car_engine.Stop();
        right_car_tire.Stop();
    }

}

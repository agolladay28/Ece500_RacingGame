using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class pause_menu : MonoBehaviour
{


    public UIDocument pause_ui;
    public audio_manager audio_manager;
    public PrometeoCarController left_car_controller;
    public PrometeoCarController right_car_controller;
    public user_prefs user_prefs;

    private VisualElement pause_doc_root;
    private Slider engine_vol_slider;
    private bool is_paused = false;
    void OnEnable()
    {
        pause_doc_root = pause_ui.rootVisualElement;
        pause_doc_root.style.display = DisplayStyle.None;
        engine_vol_slider = pause_doc_root.Q<Slider>("engine_vol_slider");

        var restart_button = pause_doc_root.Q<Button>("restart_race_button");
        restart_button.clicked += restart_race;

        var main_menu_button = pause_doc_root.Q<Button>("main_menu_button");
        main_menu_button.clicked += return_to_main_menu;


        var toggle_fullscreen_button = pause_doc_root.Q<Button>("toggle_fullscreen_button");
        toggle_fullscreen_button.clicked += user_prefs.toggle_fullscreen;

        engine_vol_slider.value = audio_manager.get_car_volume();
        engine_vol_slider.lowValue = 0;
        engine_vol_slider.highValue = 1;
        engine_vol_slider.RegisterValueChangedCallback(on_engine_vol_changed);
    }
    public void request_pause()
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
        resume_cars();
        is_paused = false;
        Time.timeScale = 1;
        pause_doc_root.style.display = DisplayStyle.None;
    }
    private void pause()
    {
        is_paused = true;
        pause_cars();
        Time.timeScale = 0;
        pause_doc_root.style.display = DisplayStyle.Flex;
    }
    public void restart_race()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Track");
    }
    public void return_to_main_menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Page");

    }
    private void on_engine_vol_changed(ChangeEvent<float> evt)
    {
        float car_volume = evt.newValue;
        audio_manager.update_car_volume(car_volume);
    }
    public void resume_cars()
    {
        left_car_controller.enabled = true;
        right_car_controller.enabled = true;
        audio_manager.car_audio_play();
    }
    public void pause_cars()
    {
        left_car_controller.enabled = false;
        right_car_controller.enabled = false;
        audio_manager.car_audio_stop();
    }
}

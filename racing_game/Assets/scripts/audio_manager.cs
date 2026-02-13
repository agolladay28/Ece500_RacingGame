using UnityEngine;

public class audio_manager : MonoBehaviour
{

    public AudioSource left_car_engine;
    public AudioSource left_car_tire;
    public AudioSource right_car_engine;
    public AudioSource right_car_tire;
    public AudioSource announcer;
    public AudioClip left_wins;
    public AudioClip right_wins;

    private const string car_volume_pref_str = "car_volume";

    void OnEnable()
    {
        car_audio_stop();
        update_car_volume(get_car_volume());
        car_audio_play();
    }
    public float get_car_volume()
    {
        float vol;
        if (!PlayerPrefs.HasKey(car_volume_pref_str))
        {
            vol = 0.7f; //default value
        }
        else
        {
            vol = PlayerPrefs.GetFloat(car_volume_pref_str);
        }
        return vol;
    }
    public void update_car_volume(float car_volume)
    {
        left_car_engine.volume = car_volume;
        right_car_engine.volume = car_volume;
        PlayerPrefs.SetFloat(car_volume_pref_str, car_volume);
        PlayerPrefs.Save();
    }
    public void car_audio_stop()
    {

        left_car_engine.Stop();
        left_car_tire.Stop();
        right_car_engine.Stop();
        right_car_tire.Stop();
    }
    public void car_audio_play()
    {

        left_car_engine.Play();
        left_car_tire.Play();
        right_car_engine.Play();
        right_car_tire.Play();
    }
    public void announce_winner(string winner_car_color)
    {
        if (winner_car_color == "Yellow")
        {
            announcer.pitch = 0.85f; //the "yellow wins!" audio clip sounds sped up, to fix we slow it down here.
            announcer.PlayOneShot(right_wins, 1.2f);
        }
        else
        {
            announcer.PlayOneShot(left_wins, 1.2f);
        }
    }
}

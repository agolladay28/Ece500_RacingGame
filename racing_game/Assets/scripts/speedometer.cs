using System;
using UnityEngine;
using UnityEngine.UIElements;

public class speedometer : MonoBehaviour
{
    public UIDocument document;
    public VisualElement spedometer_root;
    //max speed is what is shown when needle is at max
    const float max_speed = 120;

    private float max_angle = 89;
    private float min_angle = -89;

    [SerializeField] private string player_visual_element_query = "left_player";
    public Rigidbody body_to_measure;
    private float SPEED_SCALING = 6;
    [Range(0, max_speed)] private float current_speed = 50;
    VisualElement needle;
    Label speed_text;
    void OnEnable()
    {

        var doc_root = document.rootVisualElement;
        var player_root = doc_root.Q<VisualElement>(player_visual_element_query);
        needle = player_root.Q<VisualElement>("needle");
        speed_text = player_root.Q<Label>("speedometer_text");
    }


    // Update is called once per frame
    void Update()
    {
        update_speed();
        update_needle();
        speed_text.text = current_speed.ToString("F1");
    }
    private void update_speed()
    {
        current_speed = body_to_measure.linearVelocity.magnitude * SPEED_SCALING;
    }
    private void update_needle()
    {
        //center the value of needle_pos around 0
        float needle_pos = current_speed / max_speed;
        needle_pos = Mathf.Lerp(min_angle, max_angle, needle_pos);
        this.needle.style.rotate = new Rotate(new Angle(needle_pos, AngleUnit.Degree));
    }
}

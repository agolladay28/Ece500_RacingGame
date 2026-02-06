using UnityEngine;

public class player_keybinds : MonoBehaviour
{
    public PrometeoCarController car_controller;
    public KeyCode forward_key, reverse_key, left_key, right_key, handbrake_key;
    void OnEnable()
    {
        car_controller.forward_key = forward_key;
        car_controller.reverse_key = reverse_key;
        car_controller.left_key = left_key;
        car_controller.right_key = right_key;
        car_controller.handbrake_key = handbrake_key;
    }
}

using System;
using UnityEngine;

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
    private float winner_time = 0;
    void FixedUpdate()
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
            if (winner_info == left_car_info)
            {
                Debug.Log($"left car won.{winner_time}.2f");
                Time.timeScale = 0;
            }
            if (winner_info == right_car_info)
            {
                Debug.Log($"right car won.{winner_time}.2f");
                Time.timeScale = 0;
            }
        }
    }
    void update_car(race_info info, int checkpoint_number)
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
            foreach (long time in info.lap_times)
            {
                winner_time += (float)time / 1000.0f;
            }
        }

    }

}

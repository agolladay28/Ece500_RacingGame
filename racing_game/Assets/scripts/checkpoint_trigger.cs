using System;
using UnityEngine;

public class checkpoint_trigger : MonoBehaviour
{
    public race_judge race_judge;

    void OnTriggerEnter(Collider other)
    {
        if (int.TryParse(gameObject.name, out int checkpoint_number))
        {
            if (other.name == "left_car_body")
            {
                race_judge.left_car_checkpoint = checkpoint_number;
            }
            if (other.name == "right_car_body")
            {
                race_judge.right_car_checkpoint = checkpoint_number;
            }
        }
        else
        {
            Debug.LogError("cannot get int for checkpoint named:" + gameObject.name);
        }

    }


}

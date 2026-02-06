using UnityEngine;
using Cinemachine;

public class split_screen_camera_setup : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerVCam;
    void OnEnable()
    {
        set_vcam_visibility(true);
    }

    void OnDisable()
    {
        set_vcam_visibility(false);
    }

    private void set_vcam_visibility(bool enable)
    {
        if (playerVCam != null)
        {
            playerVCam.gameObject.SetActive(enable);
        }
    }
}

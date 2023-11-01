using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] public Volume volume;
    [SerializeField] public VolumeProfile newProfile;
    [SerializeField] Color32 bgColor;
    Color32 oldColor;
    VolumeProfile oldProfile;
    Camera mainCamera;

    public void NewVolume()
    {
        oldProfile = volume.profile;
        volume.profile = newProfile;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        oldColor = bgColor;
        mainCamera.backgroundColor = bgColor;

    }

    public void ResetProfile()
    {
        if (volume.profile != oldProfile)
            volume.profile = oldProfile;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCamera.backgroundColor = oldColor;


    }
}

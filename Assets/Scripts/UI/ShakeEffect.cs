using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ShakeEffect : MonoBehaviour
{
    public ShakeIntensityType shakeIntensityType;
    public float perpetualStrength, perpetualTime;
    public enum ShakeIntensityType
    {
        SuperWeak,
        Weak,
        Medium,
        Strong,
        SuperStrong,
        Perpetual

    }
    private void ShakeTypeSelect(ShakeIntensityType intensityType)
    {
        //Debug.Log("Entrou tipo shake");
        switch (intensityType)
        {
            case ShakeIntensityType.SuperWeak:
                CameraShake.Instance.ShakeCamera(2f, .1f);
                break;
            case ShakeIntensityType.Weak:
                CameraShake.Instance.ShakeCamera(5f, .1f);
                break;
            case ShakeIntensityType.Medium:
                CameraShake.Instance.ShakeCamera(8f, .1f);
                break;
            case ShakeIntensityType.Strong:
                CameraShake.Instance.ShakeCamera(11f, .1f);
                break;
            case ShakeIntensityType.SuperStrong:
                CameraShake.Instance.ShakeCamera(14f, .1f);
                break;
            case ShakeIntensityType.Perpetual:
                CameraShake.Instance.ShakeCamera(perpetualStrength, perpetualTime);
                break;
            default:
                Debug.Log("Erro na intensidade");
                break;
        }
    }

    public void Shake()
    {
        ShakeTypeSelect(shakeIntensityType);
    }
}

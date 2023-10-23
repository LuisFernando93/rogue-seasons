using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pause Menu", menuName = "Pause Menu Template")]
public class PauseMenuAssets : ScriptableObject
{
    public string tag;

    public Sprite options, exit, back;
    public string volumeSFX, volumeMusic, volumeMaster, language;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu", menuName = "Main Menu Template")]
public class MainMenuAssets : ScriptableObject
{
    public string tag;

    public Sprite play, options, credits, exit, back;
    public string about;
}

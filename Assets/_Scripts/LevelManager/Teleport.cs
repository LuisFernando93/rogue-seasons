using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : InteractEvent
{
    [SerializeField] public string SceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : InteractEvent
{
    string SceneName;

    public void LoadScene()
    {
        NextScene();
        SceneManager.LoadScene(SceneName);
    }

    private void NextScene() {

        string sceneName = SceneManager.GetActiveScene().name;
        
        switch (sceneName)
        {
            case "Hub":
                SceneName = "Summer1";
                break;
            case "Summer1":
                SceneName = "Summer2";
                break;
            case "Summer2":
                SceneName = "SummerBoss";
                break;
        }
    }

    public void CallTransicion()
    {
        GetComponent<Animator>().Play("Start");
    }


}

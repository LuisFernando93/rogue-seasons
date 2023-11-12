using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : InteractEvent
{
    string SceneName;
    [SerializeField] GameObject sceneTransition;
    [SerializeField] Sprite interactIconSprite;

    private void Start()
    {
        Instantiate(sceneTransition,GameObject.FindGameObjectWithTag("Canvas").transform);
    }
    private void LoadScene()
    {
        NextScene();
        SceneManager.LoadScene(SceneName);
    }

    private void NextScene() {

        string sceneName = SceneManager.GetActiveScene().name;
        Player.Instance.fullHeal();
        
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
            case "SummerBoss":
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Destroy(GameObject.FindGameObjectWithTag("Canvas"));
                SceneName = "Hub";
                break;
        }
    }

    public void CallTransicion()
    {
        GetComponent<Animator>().Play("Start");
        GetComponent<Collider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CloseInteractableIcon();
    }

    public void ToBoss()
    {
        CallTransicion();
        SceneManager.LoadScene("SummerBoss");
    }

}

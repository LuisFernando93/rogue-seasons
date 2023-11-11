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
        //medida para evitar o bug do interact icon não desaparecer na sala do boss
        /*if (SceneManager.GetActiveScene().name != "SummerBoss") 
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = interactIconSprite;*/
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

                //medida para evitar o bug do interact icon não desaparecer na sala do boss
                /*GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = null;
                Debug.Log("Sprite retirado");*/
                break;
            case "SummerBoss":
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

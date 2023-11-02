using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    string currentAnimation;
    Animator animator;
    [SerializeField] AnimationClip toSummer1, toSummer2, toSummerBoss, toHub;

    void Start()
    {
        animator = GetComponent<Animator>();
        NextScene();
        Destroy(this.gameObject, 2.5f);
    }

    private void NextScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            /*case "Hub":
                ChangeAnimation(toSummer1.name);
                break;*/
            case "Summer1":
                ChangeAnimation(toSummer1.name);
                break;
            case "Summer2":
                ChangeAnimation(toSummer2.name);
                break;
            case "SummerBoss":
                ChangeAnimation(toSummerBoss.name);
                break;
        }
    }

    void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimation = newAnimation;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Player player;
    Animator animator;
    private string currentAnimation;
    private string IDLE_ANIMATION = "PlayerIdle";
    private string WALK_ANIMATION = "PlayerWalk";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.movement.x != 0 || player.movement.y != 0)
        {
            ChangeAnimation(WALK_ANIMATION);
        }
        else
        {
            ChangeAnimation(IDLE_ANIMATION);
        }
    }

    public void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimation = newAnimation;

    }
}

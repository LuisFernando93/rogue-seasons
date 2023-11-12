using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Player player;
    SpriteRenderer spriteRenderer;
    Animator animator;
    DialogueUI dialogueUI;

    [SerializeField] AnimationClip playerTeleport;

    private string currentAnimation;
    private string IDLE_ANIMATION = "PlayerIdle";
    private string WALK_ANIMATION = "PlayerWalk";

    private float damageFlashTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeAnimation(IDLE_ANIMATION);
        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gameIsPaused && !GameOver.isOver)
        {
            if (player.movement.x != 0 || player.movement.y != 0)
            {
                ChangeAnimation(WALK_ANIMATION);
            }
            else
            {
                ChangeAnimation(IDLE_ANIMATION);
            }

            if (damageFlashTimer > 0f)
            {
                // Atualize a cor para vermelho
                spriteRenderer.color = Color.red;

                // Reduza o temporizador
                damageFlashTimer -= Time.deltaTime;

                // Verifique se o temporizador expirou
                if (damageFlashTimer <= 0f)
                {
                    // Retorna à cor original
                    spriteRenderer.color = Color.white;
                }
            }
        }
    }

    public void TeleportAnimation()
    {
        ChangeAnimation(playerTeleport.name);
    }

    public void DamageIndicator()
    {
        damageFlashTimer = 0.3f;
    }

    public void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimation = newAnimation;

    }
}

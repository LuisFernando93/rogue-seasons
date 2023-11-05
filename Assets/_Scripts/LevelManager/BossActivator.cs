using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab, entrance;
    [SerializeField] DialogueActivator dialogue;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] Collider2D boxCollider;
    [SerializeField] ChangeParticleColor changeParticleColor;
    [SerializeField] Color32 bossBackgroundColor;

    private void Start()
    {
        changeParticleColor.SetNewBackgroundColor(bossBackgroundColor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boxCollider.enabled = false;

            dialogue.Interact();
        }
    }

    public void SpawnBoss()
    {
        bossPrefab.SetActive(true);
        entrance.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab, entrance;
    [SerializeField] DialogueActivator dialogue;
    DialogueUI dialogueUI;
    [SerializeField] Collider2D boxCollider;
    [SerializeField] ChangeParticleColor changeParticleColor;
    [SerializeField] Color32 bossBackgroundColor;
    bool bossSpawned = false;

    private void Start()
    {
        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
        GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("Entrance").transform.position;
        changeParticleColor.SetNewBackgroundColor(bossBackgroundColor);
    }

    /*private void Update()
    {
        if (dialogueUI.IsOpen) return;
        if (bossSpawned) SpawnBoss();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.Interact();
            boxCollider.enabled = false;
            dialogue.DesactiveInteracIcon();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CloseInteractableIcon();
        }
    }

    public void SpawnBoss()
    {
        Debug.Log("Tentou Spawnar o Boss");
        bossPrefab.SetActive(true);
        entrance.SetActive(true);
        Debug.Log("Boss spawnado");
    }
}

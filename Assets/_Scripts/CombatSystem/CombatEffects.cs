using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEffects : MonoBehaviour
{
    /*[SerializeField] public bool suction;
    [SerializeField] public float suctionSpeed = 4f;
    Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o jogador entrou na zona de suc��o
        if (other.CompareTag("Player"))
        {
            Suction(other);
        }
    }

    private void Suction(Collider2D other)
    {
        if (suction)
        {
            //Debug.Log("Suc");

            // Obtenha a posi��o do centro da zona de suc��o
            Vector2 suctionCenter = transform.position;

            // Obtenha a posi��o atual do jogador
            Vector2 playerPosition = other.transform.position;

            // Calcule a dire��o do jogador em rela��o ao centro da zona de suc��o
            Vector2 suctionDirection = (suctionCenter - playerPosition).normalized;

            // Aplique a for�a de suc��o ao jogador
            playerRB.velocity = suctionDirection * suctionSpeed;
        }
       
    }*/
}

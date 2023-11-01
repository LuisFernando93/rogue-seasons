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
        // Verifique se o jogador entrou na zona de sucção
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

            // Obtenha a posição do centro da zona de sucção
            Vector2 suctionCenter = transform.position;

            // Obtenha a posição atual do jogador
            Vector2 playerPosition = other.transform.position;

            // Calcule a direção do jogador em relação ao centro da zona de sucção
            Vector2 suctionDirection = (suctionCenter - playerPosition).normalized;

            // Aplique a força de sucção ao jogador
            playerRB.velocity = suctionDirection * suctionSpeed;
        }
       
    }*/
}

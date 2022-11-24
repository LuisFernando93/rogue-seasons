using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private Player player;
    private int playerLife;
    [SerializeField] private Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdateHealth();
    }

    // Update is called once per frame
    public void UpdateHealth()
    {
        playerLife = player.GetLife();
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerLife)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }
}

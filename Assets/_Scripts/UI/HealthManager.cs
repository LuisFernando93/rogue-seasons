using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private Player player;
    private int playerLife;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerLife = player.GetLife();
        UpdateHealth();
    }

    // Update is called once per frame
    public void UpdateHealth()
    {
        playerLife = player.GetLife();
        slider.value = playerLife;       
    }
}

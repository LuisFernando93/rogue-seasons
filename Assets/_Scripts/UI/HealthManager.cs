using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    private GameObject player;
    private float playerLife;
    [SerializeField] Slider slider;
    [SerializeField] Transform lifeValue;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        if (player.GetComponent<Player>().GetLife() < 0) { playerLife = 0; } else playerLife = player.GetComponent<Player>().GetLife();
        slider.maxValue = player.GetComponent<Player>().GetMaxLife();
        slider.value = playerLife;
        //Debug.Log(playerLife+" / "+player.GetMaxLife());
        lifeValue.GetComponent<TextMeshProUGUI>().text = (playerLife+"/"+player.GetComponent<Player>().GetMaxLife());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    private Player player;
    private float playerLife;
    [SerializeField] Slider slider;
    [SerializeField] Transform lifeValue;

    // Update is called once per frame
    public void UpdateHealth()
    {
        player = GetComponent<Player>();
        playerLife = player.GetLife();
        slider.maxValue = player.GetMaxLife();
        slider.value = playerLife;
        lifeValue.GetComponent<TextMeshProUGUI>().text = (playerLife+"/"+player.GetMaxLife());
    }
}

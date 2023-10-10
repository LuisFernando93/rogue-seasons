using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] WeightedRandomList<GameObject> chestsPrefab;
    LevelManager levelManager;
    [SerializeField] int maxChest = 3, dropChance = 10;
    int chestCount = 0;

    public static ChestManager Instance;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    public void InstantiateChest(bool isChestRoom, Transform chestSpot)
    {
        if (levelManager.roomsCleared < levelManager.totalRooms && chestCount < maxChest)
        {
            if (Random.Range(1, dropChance) == 1)
            {
                if (!isChestRoom) Instantiate(chestsPrefab.GetRandom(), chestSpot.position, chestSpot.rotation);
                chestCount++;
                Debug.Log("Bau criado, quantidade de baus criados: "+chestCount);
            }
            else Debug.Log("Bau n�o foi criado");
        }
        else if (chestCount == 0)
        {
            Debug.Log("Nenhum bau criado durante a fase, bau obrigatorio criado.");
            if (!isChestRoom) Instantiate(chestsPrefab.GetRandom(), chestSpot.position, chestSpot.rotation);
            chestCount++;
        }
        else
        {
            Debug.Log("quantidade maxima de baus criados");
        }
    }

}

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
        //Tenta criar um bau se pelo menos 1 sala ainda não foi concluida e se não alcançou o maximo de baús
        if (levelManager.roomsCleared < levelManager.totalRooms - 2 && chestCount < maxChest && !isChestRoom)
        {
            if (Random.Range(1, dropChance) == 1)
            {
                if (!isChestRoom) Instantiate(chestsPrefab.GetRandom(), chestSpot.position, chestSpot.rotation);
                chestCount++;
                Debug.Log("Bau criado, quantidade de baus criados: "+chestCount);
            }
            else Debug.Log("Bau não foi criado");
        }
        else if (isChestRoom /*&& chestCount < maxChest*/)
        {
            Instantiate(chestsPrefab.GetRandom(), chestSpot.position, chestSpot.rotation);
            //chestCount++;
            Debug.Log("Baú de sala especial criado");
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

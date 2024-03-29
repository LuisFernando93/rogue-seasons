using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] WeightedRandomList<GameObject> chestsPrefab;
    LevelManager levelManager;
    [SerializeField] int maxChest = 3, dropChance = 10;
    int chestCount = 0;
    [SerializeField] GameObject chestNotify;

    public static ChestManager Instance;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    public void InstantiateChest(bool isChestRoom, Transform chestSpot)
    {
        //Tenta criar um bau se pelo menos 1 sala ainda n�o foi concluida e se n�o alcan�ou o maximo de ba�s
        if (levelManager.roomsCleared < levelManager.totalRooms - 2 && chestCount < maxChest && !isChestRoom)
        {
            if (Random.Range(1, dropChance) == 1)
            {
                if (!isChestRoom) CreateChest(chestSpot);
                chestCount++;
                Debug.Log("Bau criado, quantidade de baus criados: "+chestCount);
            }
            else Debug.Log("Bau n�o foi criado");
        }
        else if (isChestRoom /*&& chestCount < maxChest*/)
        {
            CreateChest(chestSpot);
            //chestCount++;
            Debug.Log("Ba� de sala especial criado");
        }
        else if (chestCount == 0)
        {
            Debug.Log("Nenhum bau criado durante a fase, bau obrigatorio criado.");
            if (!isChestRoom) CreateChest(chestSpot);
            chestCount++;
        }
        else
        {
            Debug.Log("quantidade maxima de baus criados");
        }
    }

    private void CreateChest(Transform chestSpot)
    {
        Instantiate(chestsPrefab.GetRandom(), chestSpot.position, chestSpot.rotation);
        Destroy(Instantiate(chestNotify, GameObject.FindGameObjectWithTag("Canvas").transform),5);
    }

}

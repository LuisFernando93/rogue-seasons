using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private GameObject spawnContainer, doorsContainer;

    private List<GameObject> doors = new List<GameObject>();
    private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] public Transform chestSpot;
    [SerializeField] private Collider2D roomActivator;
    [SerializeField] private GameObject meleeEnemyPrefab, rangedEnemyPrefab, turretEnemyPrefab;
    [SerializeField] public bool isChestRoom;
    private bool roomSleep = true;
    private bool roomCleared = false;
    private List<GameObject> enemies = new List<GameObject>();
    private ChangeParticleColor changeParticleColor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FillDoors();
        FillSpawns();
        if (roomActivator == null)
        {
            roomActivator = GetComponent<Collider2D>();
        }
        roomActivator.isTrigger = true;
        activateDoors(false);
        changeParticleColor = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<ChangeParticleColor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!roomCleared)
        {
            if (roomSleep)
            {
                if (roomActivator.IsTouching(player.GetComponent<Collider2D>()))
                {
                    awakeRoom();
                }
            }
            else
            {
                roomControl();
            }
        }
    }



    private void awakeRoom()
    {

        roomSleep = false;
        activateDoors(true);
        spawnEnemies();
        SoundManager.Instance.ChangeToBattleMusic();
        changeParticleColor.SetBattleParticles();


    }

    private void activateDoors(bool active)
    {
        if (doors != null)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(active);
            }
        }
    }

    private void spawnEnemies()
    {
        if (spawnPoints != null)
        {
            foreach (GameObject spawnPoint in spawnPoints)
            {
                EnemyType enemyType = spawnPoint.GetComponent<SpawnPoint>().enemySpawned();
                Vector3 spawnPos = spawnPoint.transform.position;
                switch (enemyType)
                {
                    case EnemyType.Melee:
                        enemies.Add(Instantiate(meleeEnemyPrefab, spawnPos, Quaternion.identity));
                        break;
                    case EnemyType.Ranged:
                        enemies.Add(Instantiate(rangedEnemyPrefab, spawnPos, Quaternion.identity));
                        break;
                    case EnemyType.Turret:
                        enemies.Add(Instantiate(turretEnemyPrefab, spawnPos, Quaternion.identity));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void roomControl()
    {
        if (enemies.Count == 0)
        {
            activateDoors(false);
            roomCleared = true;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().RoomCleared();
            SoundManager.Instance.ChangeToDefaultMusic();
            changeParticleColor.SetExplorationParticles();
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ChestManager>().InstantiateChest(isChestRoom, chestSpot);
        }
        else
        {
            enemies.RemoveAll(GameObject => GameObject == null);
        }
    }

    private void FillDoors()
    {
        int doorCount = doorsContainer.transform.childCount;

        for (int i = 0; i < doorCount; i++)
        {
            doors.Add(doorsContainer.transform.GetChild(i).gameObject);
        }
    }

    private void FillSpawns()
    {
        int spawnCount = spawnContainer.transform.childCount;

        for (int i = 0; i < spawnCount; i++)
        {
            spawnPoints.Add(spawnContainer.transform.GetChild(i).gameObject);
        }
    }
}

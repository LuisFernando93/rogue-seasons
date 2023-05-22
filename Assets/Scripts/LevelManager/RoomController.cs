using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController: MonoBehaviour
{

    private GameObject player;

    [SerializeField] private List<GameObject> doors = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private Collider2D roomActivator;
    [SerializeField] private GameObject meleeEnemyPrefab, rangedEnemyPrefab, turretEnemyPrefab;

    private bool roomSleep = true;
    private bool roomCleared = false;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ActivateDoors(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!roomCleared)
        {
            if (roomSleep)
            {
                if (roomActivator.IsTouching(player.GetComponent<Collider2D>()))
                {
                    roomSleep = false;
                    ActivateDoors(true);
                    spawnEnemies();
                }
            }
        }
    }

    private void ActivateDoors(bool active)
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
}

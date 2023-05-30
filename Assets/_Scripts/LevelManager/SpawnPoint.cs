using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int meleeChance, rangedChance, turretChance;

    private EnemyType[] availableEnemies = null;

    // Start is called before the first frame update
    void Start()
    {
        if (meleeChance < 0)
        {
            meleeChance = 0;
        }
        if (rangedChance < 0)
        {
            rangedChance = 0;
        }
        if (turretChance < 0)
        {
            turretChance = 0;
        }

        if (meleeChance + rangedChance + turretChance > 0)
        {
            availableEnemies = new EnemyType[meleeChance + rangedChance + turretChance];
            for(int i = 0; i < meleeChance; i++)
            {
                availableEnemies[i] = EnemyType.Melee;
            }
            for (int i = 0; i < rangedChance; i++)
            {
                availableEnemies[i + meleeChance] = EnemyType.Ranged;
            }
            for (int i = 0; i < turretChance; i++)
            {
                availableEnemies[i + meleeChance + rangedChance] = EnemyType.Turret;
            }
        }
    }

    public EnemyType enemySpawned()
    {
        if(availableEnemies != null)
        {
            int rngEnemy = Random.Range(0, availableEnemies.Length);
            return availableEnemies[rngEnemy];
        } else
        {
            return EnemyType.Null;
        }
    }
}

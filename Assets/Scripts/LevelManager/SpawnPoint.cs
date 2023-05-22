using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int meleeChance, rangedChance, turretChance;

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
    }

    public EnemyType enemySpawned()
    {
        return EnemyType.Melee;
    }
}

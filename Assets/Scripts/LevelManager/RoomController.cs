using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController: MonoBehaviour
{

    private GameObject player;

    [SerializeField] private List<GameObject> doors = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private Collider2D roomActivator;

    private bool roomSleep = true;
    private bool roomCleared = false;

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

    } 
}

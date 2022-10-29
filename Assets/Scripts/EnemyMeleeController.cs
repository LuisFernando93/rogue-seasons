using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject RoomController;
    [SerializeField] private int speed = 2;
    [SerializeField] private int power = 1;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        moveEnemy();
    }

    private void moveEnemy()
    {
        if(Player != null)
        {
            direction = new Vector2(Player.transform.position.x, Player.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        }
        
    }
}

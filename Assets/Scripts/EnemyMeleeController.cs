using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject RoomController;
    [SerializeField] private int speed = 2;
    [SerializeField] private int power = 1;
    private bool faceRight = true;

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
        //flip sprite
        if (Player.transform.position.x > transform.position.x && !faceRight)
        {
            Flip();
        }
        else if (Player.transform.position.x < transform.position.x && faceRight)
        {
            Flip();
        }

        if (Player != null)
        {
            direction = new Vector2(Player.transform.position.x, Player.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        }
        
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        faceRight = !faceRight;
    }
}

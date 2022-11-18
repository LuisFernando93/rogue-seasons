using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject RoomController;
    [SerializeField] private int speed = 1;
    [SerializeField] private int power = 1;
    [SerializeField] private float minDistance = 2;
    private bool faceRight = true;
    private float distance;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(Player.transform.position, transform.position);
        //Debug.Log(distance);
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {  
        if (Player != null) {
            

            //flip sprite
            if(Player.transform.position.x > transform.position.x && !faceRight){
                Flip();
            } else if (Player.transform.position.x < transform.position.x && faceRight) {
                Flip();
            }

            direction = new Vector2(Player.transform.position.x, Player.transform.position.y);
            if (distance > minDistance) {
                transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            } else if (distance < minDistance) {
                transform.position = Vector2.MoveTowards(transform.position, direction, -1 * speed * Time.deltaTime);
            }
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

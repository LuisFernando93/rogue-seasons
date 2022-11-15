using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretController : MonoBehaviour
{

    private GameObject Player;
    [SerializeField] private GameObject RoomController;
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

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (Player != null)
        {
            if (Player.transform.position.x > transform.position.x && !faceRight)
            {
                Flip();
            }
            else if (Player.transform.position.x < transform.position.x && faceRight)
            {
                Flip();
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

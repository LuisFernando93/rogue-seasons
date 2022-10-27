using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 movement;
    public float moveSpeed = 5f;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
        if (isDashing) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
            StartCoroutine(Dash());
        } 
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        PlayerMovement();
    }

    private void PlayerMovement() {
        rb.MovePosition(rb.position + movement.normalized * (moveSpeed * Time.fixedDeltaTime));
    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(movement.x * dashPower, movement.y * dashPower);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

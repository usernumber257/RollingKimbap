using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed = 0.05f;
    [SerializeField] float moveSensitive = 0.1f;

    Vector2 movement;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x != 0 || movement.y != 0)
            anim.SetBool("IsMove", true);
        else
            anim.SetBool("IsMove", false);

        anim.SetFloat("xDir", movement.x);
        anim.SetFloat("yDir", movement.y);

        if (movement.magnitude < moveSensitive)
            return;

        rb.MovePosition(transform.position += new Vector3(movement.x, movement.y, 0f) * speed);
    }
}

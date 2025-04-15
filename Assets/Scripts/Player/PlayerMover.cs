using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed = 0.12f;
    [SerializeField] float moveSensitive = 0.1f;

    [SerializeField] GameObject hat;

    Vector2 movement;

    Rigidbody2D rb;

    //모자 SetActive 를 위함
    Holder holder;
    Server server;

    bool doMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        holder = GetComponent<Holder>();
        server = GetComponent<Server>();

        holder.OnHold += TakeOffHat;
        server.OnServe += WearHat;
    }

    private void OnDestroy()
    {
        holder.OnHold -= TakeOffHat;
        server.OnServe -= WearHat;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (!doMove)
            return;

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

        rb.MovePosition(transform.position + new Vector3(movement.x, movement.y, 0f) * speed);
    }

    void WearHat()
    {
        hat.SetActive(true);
    }

    void TakeOffHat(GameObject holdingObj)
    {
        hat.SetActive(false);
    }

    public void StopMove(bool isStop)
    {
        if (isStop)
        {
            doMove = false;
            anim.SetBool("IsMove", false);
        }
        else
            doMove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyEnum;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed = 0.09f;
    [SerializeField] float moveSensitive = 0.1f;

    [SerializeField] GameObject hat;

    Vector2 movement = Vector2.zero;

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
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
#endif

#if UNITY_IOS || UNITY_ANDROID
        if (MobileInputManager.Instance.up.isPressed)
            movement.y = 1;
        else if (MobileInputManager.Instance.down.isPressed)
            movement.y = -1;
        else
            movement.y = 0;

        if (MobileInputManager.Instance.left.isPressed)
            movement.x = -1;
        else if (MobileInputManager.Instance.right.isPressed)
            movement.x = 1;
        else 
            movement.x = 0;
#endif

        if (movement.x != 0 || movement.y != 0)
            anim.SetBool("IsMove", true);
        else
            anim.SetBool("IsMove", false);

        anim.SetFloat("xDir", movement.x);
        anim.SetFloat("yDir", movement.y);

        if (movement.x < 0)
            hat.transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            hat.transform.localScale = Vector3.one;

            /*
            if (movement.magnitude < moveSensitive)
                return;
            */

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

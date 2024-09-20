using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum State { None, Sit, Order, Eat, Exit }
    State curState;
    Transform mySeat;
    FSM fsm;
    MyEnum.FoodType myOrder;
    public MyEnum.FoodType MyOrder { get { return myOrder; } }

    [SerializeField] float speed = 0.05f;

    [Header("Order Bubble")]
    [SerializeField] GameObject orderBubble;
    [SerializeField] CompleteFood orderFood;

    void Start()
    {
        mySeat = GameObject.FindWithTag("Seat").transform;

        fsm = new FSM(new SitState(transform, mySeat, speed));


        myOrder = ChoiceMyOrder();

        curState = State.Sit;
        ChangeState(State.Sit);
    }

    private void Update()
    {
        switch (curState)
        {
            case State.Sit:
                if (IsNearSeat())
                    ChangeState(State.Order);
                break;
            case State.Order:
                break;
            case State.Eat:
                break;
            case State.Exit:
                break;
        }

        fsm.UpdateState();
    }

    void ChangeState(State nextState)
    {
        curState = nextState;

        switch (curState)
        {
            case State.Sit:
                fsm.ChangeState(new SitState(transform, mySeat, speed));
                break;
            case State.Order:
                fsm.ChangeState(new OrderState(myOrder, orderBubble, orderFood));
                break;
            case State.Eat:
                fsm.ChangeState(new EatState());
                break;
            case State.Exit:
                fsm.ChangeState(new ExitState());
                break;
        }
    }

    MyEnum.FoodType ChoiceMyOrder()
    {
        int randNum = Random.Range(0, GameManager.Data.foodCount);

        return (MyEnum.FoodType)randNum;
    }

    bool IsNearSeat()
    {
        return Vector2.Distance(transform.position, mySeat.position) < 0.5f;
    }

    bool IsReceiveMyOrder()
    {
        return false;
    }


}

public class SitState : State
{
    Transform myTransform;
    Transform mySeat;

    float speed;

    public SitState(Transform myTransform, Transform mySeat, float speed)
    {
        this.myTransform = myTransform;
        this.mySeat = mySeat;
        this.speed = speed;
    }

    public override void OnStateEnter()
    {
        myTransform.position = GameObject.FindWithTag("Exit").transform.position;
    }

    public override void OnStateUpdate()
    {
        MoveToSeat();
    }
    
    public override void OnStateExit()
    {
        Holder sitHolder = mySeat.GetComponent<Holder>();

        if (sitHolder == null)
            return;

        sitHolder.Hold(myTransform.gameObject);
    }


    void MoveToSeat()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, mySeat.position, speed);
    }
}

public class OrderState : State
{
    MyEnum.FoodType myOrder;
    GameObject orderBubble;
    CompleteFood orderFood;

    public OrderState(MyEnum.FoodType myOrder, GameObject orderBubble, CompleteFood orderFood)
    {
        this.orderBubble = orderBubble;
        this.orderFood = orderFood;
    }

    public override void OnStateEnter()
    {
        orderFood.Init(myOrder);
        orderBubble.SetActive(true);
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
        orderBubble.SetActive(false);
    }


}

public class EatState : State
{
    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}

public class ExitState : State
{
    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}

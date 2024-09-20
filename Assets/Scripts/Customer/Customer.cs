using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum State { None, Sit, Order, Eat, Exit }
    State curState;
    Seat mySeat;
    FSM fsm;
    MyEnum.FoodType myOrder;
    public MyEnum.FoodType MyOrder { get { return myOrder; } }

    Transform exit;

    [SerializeField] float speed = 0.05f;

    [Header("Order Bubble")]
    [SerializeField] GameObject orderBubble;
    [SerializeField] CompleteFood orderFood;

    void Start()
    {
        exit = GameObject.FindWithTag("Exit").transform;

        mySeat = GameObject.FindWithTag("Seat").GetComponent<Seat>();
        mySeat.OnFoodReadied += IsReceiveMyOrder;

        fsm = new FSM(new SitState(transform, mySeat, speed, exit));

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
                if (IsReceiveMyOrder())
                    ChangeState(State.Eat);
                break;
            case State.Eat:
                if (IsFoodDisappear())
                    ChangeState(State.Exit);
                break;
            case State.Exit:
                if (IsNearExit())
                    Clear();
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
                fsm.ChangeState(new SitState(transform, mySeat, speed, exit));
                break;
            case State.Order:
                fsm.ChangeState(new OrderState(myOrder, orderBubble, orderFood));
                break;
            case State.Eat:
                fsm.ChangeState(new EatState(mySeat));
                break;
            case State.Exit:
                fsm.ChangeState(new ExitState(transform, speed, exit));
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
        return Vector2.Distance(transform.position, mySeat.transform.position) < 0.1f;
    }

    bool IsReceiveMyOrder()
    {
        if (myOrder == MyEnum.FoodType.None)
            return false;

        return mySeat.MyFoodType == myOrder;
    }

    bool IsFoodDisappear()
    {
        return mySeat.ReadiedFood == null;
    }

    bool IsNearExit()
    {
        return Vector2.Distance(transform.position, exit.position) < 0.1f;
    }

    void Clear()
    {
        Debug.Log("Clear!");
        Destroy(gameObject);
    }
}

public class SitState : State
{
    Transform myTransform;
    Seat mySeat;
    Transform exit;

    float speed;

    public SitState(Transform myTransform, Seat mySeat, float speed, Transform exit)
    {
        this.myTransform = myTransform;
        this.mySeat = mySeat;
        this.speed = speed;
        this.exit = exit;
    }

    public override void OnStateEnter()
    {
        myTransform.position = exit.transform.position;
    }

    public override void OnStateUpdate()
    {
        MoveToSeat();
    }
    
    public override void OnStateExit()
    {
        mySeat.Sit(myTransform.gameObject);
    }


    void MoveToSeat()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, mySeat.transform.position, speed);
    }
}

public class OrderState : State
{
    MyEnum.FoodType myOrder;
    GameObject orderBubble;
    CompleteFood orderFood;

    public OrderState(MyEnum.FoodType myOrder, GameObject orderBubble, CompleteFood orderFood)
    {
        this.myOrder = myOrder; 
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
    Seat mySeat;
    

    public EatState(Seat mySeat)
    {
        this.mySeat = mySeat;
    }

    public override void OnStateEnter()
    {
        mySeat.ReadiedFood.Disappear();
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }
}

public class ExitState : State
{
    Transform exit;
    Transform myTransform;
    float speed;

    public ExitState(Transform myTransform, float speed, Transform exit)
    {
        this.myTransform = myTransform;
        this.speed = speed;
        this.exit = exit;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, exit.position, speed);
    }

    public override void OnStateExit()
    {
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{
    public enum State { None, Sit, Order, Eat, Exit }
    State curState;

    int myNum;
    public int MyNum { get { return myNum; } }

    int mySeatNum;
    public int MySeatNum { get { return mySeatNum; } }
    public Seat mySeat;

    FSM fsm;
    MyEnum.FoodType myOrder;
    public MyEnum.FoodType MyOrder { get { return myOrder; } }
    
    public Transform exit;

    float speed = 2f;
    public float Speed { get { return speed; } }

    [Header("Order Bubble")]
    [SerializeField] public GameObject orderBubble;
    [SerializeField] public TMP_Text orderBubbleText;
    [SerializeField] public CompleteFood orderFood;

    public enum Emotions { None, HalfAnger, FullAnger, Happy }
    [Header("Emotions")]
    [SerializeField] GameObject halfAnger;
    [SerializeField] GameObject fullAnger;
    [SerializeField] GameObject happy;
    public Emotions curEmotion = Emotions.None;

    [SerializeField] Animator anim; 

    public UnityAction OnClear;

    public int moveIndex = 0;


    public void Init(int customerNum)
    {
        myNum = customerNum;
        
        exit = GameObject.FindWithTag("Exit").transform;
        fsm = new FSM(new SitState(this));
    }

    public void WakeUp(int seatNum)
    {
        gameObject.SetActive(true);

        mySeatNum = seatNum;
        mySeat = GameManager.Seat.seats[mySeatNum];
        mySeat.OnFoodReadied += IsReceiveMyOrder;

        moveIndex = 0;

        myOrder = ChoiceMyOrder();

        curState = State.Sit;
        ChangeState(State.Sit);

        curEmotion = Emotions.None;
        halfAnger.SetActive(false);
        fullAnger.SetActive(false);
        happy.SetActive(false);
    }

    private void Update()
    {
        switch (curState)
        {
            case State.Sit:
                //if (IsNearSeat())
                if (mySeat.navPivot.Length <= moveIndex)
                    ChangeState(State.Order);
                break;
            case State.Order:
                if (IsReceiveMyOrder())
                    ChangeState(State.Eat);
                if (curEmotion == Emotions.FullAnger)
                    ChangeState(State.Exit);
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

        PlayAnimation();
    }

    Vector3 prevPosition;
    Vector3 moveDir;
    void PlayAnimation()
    {
        moveDir = gameObject.transform.position - prevPosition;

        if (moveDir != Vector3.zero)
            anim.SetBool("IsMove", true);
        else
            anim.SetBool("IsMove", false);
        
        anim.SetFloat("xDir", moveDir.x);
        anim.SetFloat("yDir", moveDir.y);

        prevPosition = gameObject.transform.position;
    }

    void ChangeState(State nextState)
    {
        curState = nextState;

        switch (curState)
        {
            case State.Sit:
                fsm.ChangeState(new SitState(this));
                break;
            case State.Order:
                fsm.ChangeState(new OrderState(this));
                break;
            case State.Eat:
                fsm.ChangeState(new EatState(this));
                break;
            case State.Exit:
                fsm.ChangeState(new ExitState(this));
                break;
        }
    }

    MyEnum.FoodType ChoiceMyOrder()
    {
        int randNum = Random.Range(1, PlayerStatManager.Instance.foodCount + 1);

        return (MyEnum.FoodType)randNum;
    }

    bool IsNearSeat()
    {
        return Vector2.Distance(transform.position, mySeat.transform.position) < 0.001f;
    }

    bool IsReceiveMyOrder()
    {
        if (myOrder == MyEnum.FoodType.None)
            return false;

        if (mySeat.ReadiedFood == null)
            return false;

        return mySeat.MyFoodType == myOrder;
    }

    bool IsFoodDisappear()
    {
        return mySeat.ReadiedFood == null;
    }

    bool IsNearExit()
    {
        return Vector2.Distance(transform.position, exit.position) < 0.01f;
    }

    void Clear()
    {
        OnClear?.Invoke();
        gameObject.SetActive(false);
    }

    public void ChangeEmotion(bool increase)
    {
        halfAnger.SetActive(false);
        fullAnger.SetActive(false);
        happy.SetActive(false);

        switch (curEmotion)
        {
            case Emotions.None:
                curEmotion = increase ? Emotions.Happy : Emotions.HalfAnger;
                break;
            case Emotions.HalfAnger:
                curEmotion = increase ? Emotions.None : Emotions.FullAnger;
                break;
            case Emotions.FullAnger:
                curEmotion = increase ? Emotions.HalfAnger : Emotions.FullAnger;
                break;
            case Emotions.Happy:
                curEmotion = increase ? Emotions.Happy : Emotions.None;
                break;
        }

        switch (curEmotion)
        {
            case Emotions.None:
                break;
            case Emotions.HalfAnger:
                halfAnger.SetActive(true);
                break;
            case Emotions.FullAnger:
                fullAnger.SetActive(true);
                break;
            case Emotions.Happy:
                happy.SetActive(true);
                break;
        }
    }
}

public class SitState : State
{
    public SitState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
        customer.transform.position = customer.mySeat.navPivot[0].position;
    }

    public override void OnStateUpdate()
    {
        MoveToSeat();
    }
    
    public override void OnStateExit()
    {
        customer.mySeat.Sit(customer.transform.gameObject);
    }

    void MoveToSeat()
    {
        Transform target = customer.mySeat.navPivot[customer.moveIndex];
        customer.transform.position = Vector2.MoveTowards(customer.transform.position, target.position, customer.Speed * Time.deltaTime);

        if (Vector2.Distance(customer.transform.position, target.position) < 0.001f)
            customer.moveIndex++;
    }
}

public class OrderState : State
{
    float waitTime = 0f;

    public OrderState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
        customer.orderFood.Init(customer.MyOrder);
        customer.orderBubble.SetActive(true);
        customer.orderBubbleText.text = SettingManager.Instance.isKor ? customer.orderFood.myFood.ItemName : customer.orderFood.myFood.ItemName_eng;
    }

    public override void OnStateUpdate()
    {
        waitTime += Time.deltaTime;

        if (waitTime > GameManager.Level.HalfAngerTime && waitTime <= GameManager.Level.FullAngerTime)
        {
            if (customer.curEmotion != Customer.Emotions.HalfAnger)
                customer.ChangeEmotion(false);
        }
        else if (waitTime > GameManager.Level.FullAngerTime)
        {
            if (customer.curEmotion != Customer.Emotions.FullAnger)
            {
                customer.ChangeEmotion(false);
            }
        }
    }

    public override void OnStateExit()
    {
        customer.orderBubble.SetActive(false);
        waitTime = 0f;
    }
}

public class EatState : State
{
    public EatState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
        PlayerStatManager.Instance.EarnCoin(customer.orderFood.myFood.Price);

        customer.ChangeEmotion(true);

        if (customer.curEmotion == Customer.Emotions.Happy)
            SoundPlayer.Instance.Play(MyEnum.Sound.Done);

        customer.mySeat.ReadiedFood.Disappear();
        customer.mySeat.Clear();
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
    public ExitState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
        customer.mySeat.Leave();
        customer.transform.SetParent(null);
        moveIndex = customer.mySeat.navPivot.Length - 1;

        customer.transform.position = customer.mySeat.navPivot[moveIndex].position;

        GameManager.Level.SetPopularity(customer.curEmotion);
    }

    public override void OnStateUpdate()
    {
        MoveToExit();
    }

    int moveIndex = 0;
    void MoveToExit()
    {
        if (moveIndex < 0)
            return;

        customer.transform.position = Vector2.MoveTowards(customer.transform.position, customer.mySeat.navPivot[moveIndex].position, customer.Speed * Time.deltaTime);

        if (Vector2.Distance(customer.transform.position, customer.mySeat.navPivot[moveIndex].position) < 0.001f)
            moveIndex--;
    }

    public override void OnStateExit()
    {
    }

}

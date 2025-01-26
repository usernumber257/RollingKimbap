using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public Seat[] seats;
    Dictionary<int, bool> emptySeats = new Dictionary<int, bool>();
    int activeSeatIndex = 1; //처음에 두 좌석은 활성화 되어있음
    SeatManager seatManager;
    public int maxSeatSize = 6;

    Customer[] customers;
    int maxCustomerSize = 12;
    Dictionary<int, bool> inactiveCustomers = new Dictionary<int, bool>();
    

    void Start()
    {
        //AddSeats();
        CreateCustomerPool();

        randTime = Random.Range(GameManager.Level.VisitTime_min, GameManager.Level.VisitTime_max);

        InitSeats();
    }

    void InitSeats()
    {
        seatManager = GameObject.FindWithTag("SeatManager").GetComponent<SeatManager>();
        seats = seatManager.seats;

        for (int i = 0; i < seats.Length; i++)
            emptySeats.Add(i, false);

        //맨 처음에 한 테이블(두 좌석)은 활성화 상태 
        emptySeats[0] = true;
        emptySeats[1] = true;
    }

    public void UnlockSeat()
    {
        seatManager.ShowNewSeat();

        activeSeatIndex += 1;
        emptySeats[activeSeatIndex] = true;
        activeSeatIndex += 1;
        emptySeats[activeSeatIndex] = true;
    }

    /*
    void AddSeats()
    {
        GameObject[] newGo = GameObject.FindGameObjectsWithTag("Seat");

        for (int i = 0; i < newGo.Length; i++)
        {
            seats.Add(newGo[i].GetComponent<Seat>());
            emptySeats.Add(i, true);
        }
    }
    */

    float time = 0;
    float randTime;

    bool newCustomerOut = false;
    private void Update()
    {
        if (time > randTime)
        {
            NewCustomer();
            newCustomerOut = true;
            randTime = Random.Range(GameManager.Level.VisitTime_min, GameManager.Level.VisitTime_max);

            time = 0;
        }

        if (!newCustomerOut)
            time += Time.deltaTime;
    }

    void CreateCustomerPool()
    {
        customers = new Customer[maxCustomerSize];
        
        for (int i = 0; i < maxCustomerSize; i++)
        {
            GameObject newGo = Instantiate(Resources.Load<GameObject>("Customer"));
            newGo.SetActive(false);
            customers[i] = newGo.GetComponent<Customer>();
            customers[i].Init(i);
            inactiveCustomers.Add(i, true);
        }
    }

    void NewCustomer()
    {
        int inactiveCustomer = FindInactiveCustomer();

        if (inactiveCustomer == -1)
            return;

        int mySeatNum = FindEmptySeat();

        if (mySeatNum == -1)
            return;

        customers[inactiveCustomer].WakeUp(mySeatNum);
        inactiveCustomers[inactiveCustomer] = false;

        emptySeats[mySeatNum] = false;

        MappingCustomerEvent(customers[inactiveCustomer]);
    }

    int FindInactiveCustomer()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            if (inactiveCustomers[i])
                return i;
        }

        return -1;
    }

    void MappingCustomerEvent(Customer who)
    {
        who.OnClear += (() => { emptySeats[who.MySeatNum] = true; newCustomerOut = false; inactiveCustomers[who.MyNum] = true; });
    }

    int limit = 30;
    int FindEmptySeat()
    {
        int limitCount = 0;

        while (true)
        {
            int i = Random.Range(0, maxCustomerSize);

            if (emptySeats[i])
                return i;

            limitCount++;
            if (limitCount > limit)
                break;
        }

        return -1;
    }
}

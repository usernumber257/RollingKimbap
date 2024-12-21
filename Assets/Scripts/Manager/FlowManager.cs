using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public List<Seat> seats = new List<Seat>();
    Dictionary<int, bool> emptySeats = new Dictionary<int, bool>(); 

    Customer[] customers;
    int poolSize = 10;
    Dictionary<int, bool> inactiveCustomers = new Dictionary<int, bool>();
    

    void Start()
    {
        AddSeats();
        CreateCustomerPool();

        randTime = Random.Range(GameManager.Level.VisitTime_min, GameManager.Level.VisitTime_max);
    }

    void AddSeats()
    {
        GameObject[] newGo = GameObject.FindGameObjectsWithTag("Seat");

        for (int i = 0; i < newGo.Length; i++)
        {
            seats.Add(newGo[i].GetComponent<Seat>());
            emptySeats.Add(i, true);
        }
    }

    float time = 0;
    float randTime;
    private void Update()
    {
        if (time > randTime)
        {
            NewCustomer();
            randTime = Random.Range(GameManager.Level.VisitTime_min, GameManager.Level.VisitTime_max);

            time = 0;
        }

        time += Time.deltaTime;
    }

    void CreateCustomerPool()
    {
        customers = new Customer[poolSize];
        
        for (int i = 0; i < poolSize; i++)
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
        who.OnClear += (() => { emptySeats[who.MySeatNum] = true; });
        who.OnClear += (() => { inactiveCustomers[who.MyNum] = true; });
    }

    int limit = 30;
    int FindEmptySeat()
    {
        int limitCount = 0;

        while (true)
        {
            int i = Random.Range(0, seats.Count);

            if (emptySeats[i])
                return i;

            limitCount++;
            if (limitCount > limit)
                break;
        }

        return -1;
    }
}

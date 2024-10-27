using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public List<Seat> seats = new List<Seat>();
    Dictionary<int, bool> emptySeats = new Dictionary<int, bool>(); 

    Customer[] customers;
    int poolSize = 4;
    Dictionary<int, bool> inactiveCustomers = new Dictionary<int, bool>();
    

    void Start()
    {
        AddSeats();
        CreateCustomerPool();
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
            randTime = Random.Range(5, 20f);

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

    int FindEmptySeat()
    {
        for (int i = 0; i < seats.Count; i++)
        {
            if (emptySeats[i])
                return i;
        }

        return -1;
    }
}

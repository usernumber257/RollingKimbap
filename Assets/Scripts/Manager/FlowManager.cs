using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    List<Seat> seats = new List<Seat>();
    Customer[] customerPool;
    int poolSize = 4;
    
    public int curActiveCustomers = 0;
    int _customerIndex = 0;
    int customerIndex 
    { 
        get { return _customerIndex; } 
        set 
        {
            _customerIndex = value;

            if (_customerIndex >= poolSize) 
                _customerIndex = 0;
        } 
    }

    int _seatIndex = 0;
    int seatIndex
    {
        get { return _seatIndex; }
        set
        {
            _seatIndex = value;

            if (_seatIndex >= seats.Count)
                _seatIndex = 0;
        }
    }

    void Start()
    {
        FindSeats();
        CreateCustomerPool();
    }

    void FindSeats()
    {
        GameObject[] newGo = GameObject.FindGameObjectsWithTag("Seat");

        for (int i = 0; i < newGo.Length; i++)
            seats.Add(newGo[i].GetComponent<Seat>());
    }

    float time = 0;
    float randTime;
    private void Update()
    {
        if (time > randTime)
        {
            NewCustomer();
            randTime = Random.Range(1, 3f);

            time = 0;
        }

        time += Time.deltaTime;

    }

    void CreateCustomerPool()
    {
        customerPool = new Customer[poolSize];
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newGo = Instantiate(Resources.Load<GameObject>("Customer"));
            newGo.SetActive(false);
            customerPool[i] = newGo.GetComponent<Customer>();
        }
    }

    void NewCustomer()
    {
        if (curActiveCustomers >= poolSize)
            return;

        customerPool[customerIndex++].Init(seats[seatIndex++]);
    }
}

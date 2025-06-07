using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �¼�, �մԵ��� ��Ȳ�� �ٷ�ϴ�. 
/// </summary>
public class SeatingManager : MonoBehaviour
{
    public Seat[] seats;
    Dictionary<int, bool> emptySeats = new Dictionary<int, bool>();
    int activeSeatIndex = 1; //ó���� �� �¼��� Ȱ��ȭ �Ǿ�����
    TableReferencer tableReferencer;
    public int maxSeatSize = 6;

    Customer[] customers;
    int maxCustomerSize = 12;
    Dictionary<int, bool> inactiveCustomers = new Dictionary<int, bool>();

    Transform customerPoolParent;
    

    void Start()
    {
        //AddSeats();
        CreateCustomerPool();

        randTime = Random.Range(GameManager.Level.VisitTime_min, GameManager.Level.VisitTime_max);

        InitSeats();
    }

    public void RegisterTableReferencer(TableReferencer tableReferencer)
    {
        this.tableReferencer = tableReferencer;
    }

    void InitSeats()
    {
        seats = tableReferencer.seats;

        for (int i = 0; i < seats.Length; i++)
            emptySeats.Add(i, false);

        //�� ó���� �� ���̺�(�� �¼�)�� Ȱ��ȭ ���� 
        emptySeats[0] = true;
        emptySeats[1] = true;
    }

    public void UnlockSeat()
    {
        tableReferencer.ShowNewSeat();

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

    /// <summary>
    /// �� ���� parent �� �� ���� ������Ʈ�� ���� �� �� �ȿ� customer ���� ������Ʈ���� Ǯ�� �����ϴ�.
    /// </summary>
    void CreateCustomerPool()
    {
        customerPoolParent = new GameObject().transform;
        customerPoolParent.name = "customer pool";

        customers = new Customer[maxCustomerSize];
        
        for (int i = 0; i < maxCustomerSize; i++)
        {
            GameObject newGo = Instantiate(Resources.Load<GameObject>("Customer"));

            customers[i] = newGo.GetComponent<Customer>();
            customers[i].Init(i);
            inactiveCustomers.Add(i, true);

            newGo.transform.parent = customerPoolParent;
            newGo.SetActive(false);
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
        who.OnClear += (() => { emptySeats[who.MySeatNum] = true; inactiveCustomers[who.MyNum] = true; });
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

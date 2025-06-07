using UnityEngine;

/// <summary>
/// 음식들의 재료를 쌓습니다
/// </summary>
[RequireComponent(typeof(Holder))]
public class FoodStacker : MonoBehaviour
{
    [Header("Kimbap")]
    public Food originalKimbap;
    public Food cheeseKimbap;
    public Food kimchiKimbap;
    public Food tunaKimbap;

    int curStack = 0;

    Food curFood;
    public Food CurFood { get { return curFood; } set { curFood = value; } }

    SpriteRenderer[] pool;
    int poolSize = 10;

    Vector3 spawnPos = new Vector3(0f, 0.02f, 0f);

    Holder holder;

    public bool makingFood;

    int randNum = 0;

    private void Awake()
    {
        holder = GetComponent<Holder>();
    }

    private void Start()
    {
        MakePool();
    }

    void MakePool()
    {
        pool = new SpriteRenderer[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            SpriteRenderer newGo = new GameObject().AddComponent<SpriteRenderer>();
            newGo.transform.parent = transform;
            newGo.transform.localPosition = spawnPos;
            newGo.gameObject.SetActive(false);
            newGo.sortingOrder = 10 + i; 
            pool[i] = newGo;
        }
    }

    public void StackIngredients()
    {
        if (CurFood == null || !makingFood)
            return;

        if (curStack < CurFood.Ingredients.Count)
        {
            randNum = Random.Range(0, 3);

            switch (randNum)
            {
                case 0:
                    SoundPlayer.Instance.Play(MyEnum.Sound.Cook1);
                    break;
                case 1:
                    SoundPlayer.Instance.Play(MyEnum.Sound.Cook2);
                    break;
                case 2:
                    SoundPlayer.Instance.Play(MyEnum.Sound.Cook3);
                    break;
            }

            pool[curStack].sprite = curFood.Ingredients[curStack].Model;
            pool[curStack].gameObject.SetActive(true);
        }
        
        curStack++;
    }

    public void Complete()
    {
        if (CurFood == null)
            return;

        SoundPlayer.Instance.Play(MyEnum.Sound.Done);

        CompleteFood newFood = Instantiate(Resources.Load<CompleteFood>("CompleteFood"));
        newFood.Init(CurFood.FoodType);

        holder.Hold(newFood.gameObject);

        //김밥이 바로 안 집히는 경우가 있음
        holder.GetComponent<Collider2D>().enabled = false;
        holder.GetComponent<Collider2D>().enabled = true;

        Stop();
    }

    public void Stop()
    {
        DisactiveAllIngredients();

        curFood = null;
        makingFood = false;

        curStack = 0;
    }

    void DisactiveAllIngredients()
    {
        for (int i = 0; i < poolSize; i++)
            pool[i].gameObject.SetActive(false);
    }
}

using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class KimbapMaker : Maker
{
    InteractableObject interactableObject;
    GameObject player;

    private new void Awake()
    {
        base.Awake();

        interactableObject = GetComponent<InteractableObject>();
        interactableObject.OnInteract += Make;

        player = GameObject.FindWithTag("Player");
    }

    public void Make(bool isInteracted)
    {
        if (!isInteracted || FoodStacker.CurFood == null)
        {
            StopMake();
            FoodStacker.Stop();

            Done();

            return;
        }

        OnKeyDown += FoodStacker.StackIngredients;

        player.GetComponent<PlayerMover>().StopMove(true);
        Minigame_WASD(FoodStacker.CurFood.Ingredients.Count + 1); //��� �� + 1 �ؾ� �ϼ��ǰ�
    }

    public override void Done()
    {
        base.Done();

        OnKeyDown -= FoodStacker.StackIngredients;

        player.GetComponent<PlayerMover>().StopMove(false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (!isInteracted || FoodStacker.curFood == null)
        {
            StopMake();
            FoodStacker.Stop();

            Done();

            return;
        }

        OnKeyDown += FoodStacker.StackIngredients;
        OnClear += FoodStacker.Complete; 

        OnClear += Done;

        player.GetComponent<PlayerMover>().StopMove(true);
        Minigame_WASD(FoodStacker.curFood.Ingredients.Count + 1); //재료 수 + 1 해야 완성되게
    }

    public void Done()
    {
        OnKeyDown -= FoodStacker.StackIngredients;
        OnClear -= FoodStacker.Complete;

        player.GetComponent<PlayerMover>().StopMove(false);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }
    
}

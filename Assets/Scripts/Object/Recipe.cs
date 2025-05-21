using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] GameObject body;
    [SerializeField] GameObject koreanRecipe;
    [SerializeField] GameObject engRecipe;

    InteractableObject interactable;

    private void Awake()
    {
        interactable = GetComponent<InteractableObject>();

        if (interactable == null)
            return;

        interactable.OnInteract += OpenRecipe;

        body.SetActive(false);
    }

    private void OnDestroy()
    {
        interactable.OnInteract -= OpenRecipe;
    }

    private void Update()
    {
        if (body.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            body.SetActive(false);
    }


    void OpenRecipe(bool isOpen)
    {
        if (isOpen)
        {
            body.SetActive(true);

            koreanRecipe.SetActive(false);
            engRecipe.SetActive(false);

            if (GameManager.Setting.isKor)
                koreanRecipe.SetActive(true);
            else
                engRecipe.SetActive(true);
        }
        else
            body.SetActive(false);
    }
}

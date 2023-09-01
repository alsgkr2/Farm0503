using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeOpen : MonoBehaviour
{

    [SerializeField] private UIManager ui;
    private bool triger = false;

    void Update()
    {
        openRecipe();
        
    }

    public void openRecipe()
    {
        if (triger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (ui.cooking_state == true)
                {
                    ui.Cooking_Recipe.SetActive(false);
                    ui.cooking_state = false;
                }
                else if (ui.cooking_state == false)
                {
                    ui.Cooking_Recipe.SetActive(true);
                    ui.cooking_state = true;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        triger = false;
    }
}

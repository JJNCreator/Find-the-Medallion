using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public bool interactable = false;
    private Transform transformCache;
    
    public static Potion current;

    private void OnEnable()
    {
        PlayerController.onInteraction += ConsumePotion;
    }
    private void OnDisable()
    {
        PlayerController.onInteraction -= ConsumePotion;
    }

    private void ConsumePotion()
    {
        if (!interactable)
            return;

        GameManager.Instance.RevealThief();
        GameUIManager.Instance.ToggleInteractObject(false);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider){
        Debug.Log(collider.gameObject.name);
        
        if(collider.gameObject.name == "Character"){
            GameUIManager.Instance.ToggleInteractObject(true);
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        Debug.Log(collider.gameObject.name);

        if (collider.gameObject.name == "Character")
        {
            GameUIManager.Instance.ToggleInteractObject(false);
            interactable = false;
        }
    }
}

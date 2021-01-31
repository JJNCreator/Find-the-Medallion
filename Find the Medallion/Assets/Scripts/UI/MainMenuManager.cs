using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public enum MenuState
    {
        Main,
        SelectLevel,
        Instructions,
        Credits
    }
    public GameObject mainButtons;
    public GameObject selectLevelObject;
    public GameObject instructionsObject;
    public GameObject creditsObject;
    public GameObject backButton;


    [SerializeField]
    private MenuState currentMenuState;
    public MenuState CurrentMenuState
    {
        get
        {
            return currentMenuState;
        }
        set
        {
            currentMenuState = value;
            OnMenuChanged(currentMenuState);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentMenuState = MenuState.Main;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentMenuState != MenuState.Main)
        {
            if(Input.GetButtonUp("Back"))
            {
                BackButton();
            }
        }
    }
    public void OpenLevelSelection()
    {
        CurrentMenuState = MenuState.SelectLevel;
    }
    public void OpenInstructions()
    {
        CurrentMenuState = MenuState.Instructions;
    }
    public void OpenCredits()
    {
        CurrentMenuState = MenuState.Credits;
    }
    public void SelectLevel(int level)
    {
        StartCoroutine(StartLoadingGameScene(level));
    }
    private IEnumerator StartLoadingGameScene(int level)
    {
        AsyncOperation aOperation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);

        while(!aOperation.isDone)
        {
            //TODO: put progress bar code here
            yield return null;
        }
    }
    private void OnMenuChanged(MenuState newState)
    {
        switch(newState)
        {
            case MenuState.Main:
                if(backButton != null)
                {
                    backButton.SetActive(false);
                }
                ToggleMenuObjects(true, false, false, false);
                EventSystem.current.SetSelectedGameObject(mainButtons.transform.Find("Play").gameObject);
                break;
            case MenuState.SelectLevel:
                if (backButton != null)
                {
                    backButton.SetActive(true);
                }
                ToggleMenuObjects(false, true, false, false);
                EventSystem.current.SetSelectedGameObject(selectLevelObject.transform.Find("Option1").gameObject);
                break;
            case MenuState.Instructions:
                if (backButton != null)
                {
                    backButton.SetActive(true);
                }
                ToggleMenuObjects(false, false, true, false);
                //EventSystem.current.SetSelectedGameObject(backButton);
                break;
            case MenuState.Credits:
                if (backButton != null)
                {
                    backButton.SetActive(true);
                }
                ToggleMenuObjects(false, false, false, true);
                //EventSystem.current.SetSelectedGameObject(backButton);
                break;
        }
    }
    public void BackButton()
    {
        switch(CurrentMenuState)
        {
            case MenuState.SelectLevel:
                CurrentMenuState = MenuState.Main;
                EventSystem.current.SetSelectedGameObject(mainButtons.transform.Find("Play").gameObject);
                break;
            case MenuState.Instructions:
                CurrentMenuState = MenuState.Main;
                EventSystem.current.SetSelectedGameObject(mainButtons.transform.Find("Instructions").gameObject);
                break;
            case MenuState.Credits:
                CurrentMenuState = MenuState.Main;
                EventSystem.current.SetSelectedGameObject(mainButtons.transform.Find("Credits").gameObject);
                break;
        }
    }
    private void ToggleMenuObjects(bool main, bool levelSelect, bool instructions, bool credits)
    {
        if(mainButtons != null)
        {
            mainButtons.SetActive(main);
        }
        if(selectLevelObject != null)
        {
            selectLevelObject.SetActive(levelSelect);
        }
        if(instructionsObject != null)
        {
            instructionsObject.SetActive(instructions);
        }
        if(creditsObject != null)
        {
            creditsObject.SetActive(credits);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject interactObject;
    public GameObject gameCompleteObject;
    public GameObject gameOverObject;
    public Text timeText;

    private static GameUIManager instance;
    public static GameUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameUIManager>();
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReloadScene()
    {
        StartCoroutine(StartReloadingGame());
    }
    public void ReturnToMainMenu()
    {
        StartCoroutine(StartLoadingMainMenu());
    }
    private IEnumerator StartReloadingGame()
    {
        AsyncOperation aOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        while(!aOperation.isDone)
        {
            yield return null;
        }
    }
    private IEnumerator StartLoadingMainMenu()
    {
        AsyncOperation aOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

        while (!aOperation.isDone)
        {
            yield return null;
        }
    }
    public void ToggleInteractObject(bool toggle)
    {
        if(interactObject != null)
        {
            interactObject.SetActive(toggle);
        }
    }
    public void ToggleGameCompleteObject()
    {
        if(gameCompleteObject != null)
        {
            gameCompleteObject.SetActive(true);
        }
    }
    public void ToggleGameOverObject()
    {
        if (gameOverObject != null)
        {
            gameOverObject.SetActive(true);
        }
    }
}

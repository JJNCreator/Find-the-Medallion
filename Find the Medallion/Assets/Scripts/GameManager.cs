using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawn;
    public Transform aiSpawn;
    
    public GameObject player;
    public GameObject thief;
    public GameObject potion;
    public Transform potionSpawnPoints;

    public float thiefVisibilityDuration = 5f;
    
    public GameObject[] aiWaypoints;
    
    int numberOfSpawnedPotions = 0;
    int maxSpawnedPotions = 0;
    
    float lastPotionSpawnTime = 0;
    float potionSpawnRate = 1;
    
    // Start is called before the first frame update
    private static GameManager instance;
    public static GameManager Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<GameManager>();
            }
            
            return instance;
        }
    }
    
    void Start()
    {
        foreach(Transform spawnPoint in potionSpawnPoints){
            if(spawnPoint.gameObject.CompareTag("InactivePotionSpawn")){
                
                Instantiate(potion, spawnPoint);
                
                spawnPoint.gameObject.tag = "ActivePotionSpawn";
            }
        }

        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        aiSpawn = GameObject.FindGameObjectWithTag("AISpawn").transform;
        aiWaypoints = GameObject.FindGameObjectsWithTag("AIWaypoint");

        SpawnPlayer();
        SpawnPotion();
        SpawnAI();
    }
    
    void Update(){
        if(Time.realtimeSinceStartup - lastPotionSpawnTime >= potionSpawnRate){
            if(numberOfSpawnedPotions < maxSpawnedPotions){
                SpawnPotion();
            }
            
            lastPotionSpawnTime = Time.realtimeSinceStartup;
        }
    }
    public void RevealThief()
    {
        Debug.Log("RevealingThief");
        
        numberOfSpawnedPotions--;
        
        StartCoroutine(XrayDuration());
    }

    public void GameComplete()
    {
        GameUIManager.Instance.ToggleGameCompleteObject();
        GameUIManager.Instance.ToggleInteractObject(false);
        player.GetComponent<PlayerController>().enabled = false;
        StopCoroutine(XrayDuration());
        Invoke("LoadMainMenu", 5f);
    }

    public void GameOver()
    {
        GameUIManager.Instance.ToggleGameOverObject();
        GameUIManager.Instance.ToggleInteractObject(false);
        player.GetComponent<PlayerController>().enabled = false;
        thief.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
    }

    private void LoadMainMenu()
    {
        GameUIManager.Instance.ReturnToMainMenu();
    }


    private IEnumerator XrayDuration()
    {
        player.GetComponent<PlayerController>().canSeeInvisible = true;

        thief.GetComponentInChildren<SkinnedMeshRenderer>().enabled = player.GetComponent<PlayerController>().canSeeInvisible;

        yield return new WaitForSeconds(thiefVisibilityDuration);

        player.GetComponent<PlayerController>().canSeeInvisible = false;
        thief.GetComponentInChildren<SkinnedMeshRenderer>().enabled = player.GetComponent<PlayerController>().canSeeInvisible;
        GameUIManager.Instance.ToggleInteractObject(false);

        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
        SpawnPotion();
    }

    private void SpawnPlayer()
    {
        player = (GameObject)Resources.Load("Player");
        GameObject playerInstance = (GameObject)Instantiate(player, playerSpawn.transform.position, playerSpawn.transform.rotation);
        
        playerInstance.gameObject.name = "Character";
        
        FindObjectOfType<CameraMovement>().target = playerInstance.transform;

        player = playerInstance;
    }
    private void SpawnAI()
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("Thief"), aiSpawn.transform.position, aiSpawn.transform.rotation);
        thief = go;
        thief.name = "Thief";
    }
    public void SpawnPotion()
    {
        foreach(Transform spawnPoint in potionSpawnPoints){
            if(spawnPoint.gameObject.CompareTag("InactivePotionSpawn")){
                
                Instantiate(potion, spawnPoint);
                
                spawnPoint.gameObject.tag = "ActivePotionSpawn";
                
                numberOfSpawnedPotions++;
                
                break;
            }
        }
    }
}

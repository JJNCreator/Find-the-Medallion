using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AI : MonoBehaviour
{
    public bool interactable = false;
    private NavMeshAgent agent;
    private Animator aniCom;
    private int currentWaypointIndex = 0;
    private GameObject[] aiWaypoints;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        aniCom = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        aiWaypoints = GameObject.FindGameObjectsWithTag("AIWaypoint");
        agent.autoBraking = false;
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
        aniCom.SetFloat("Speed", Mathf.Abs(agent.velocity.magnitude));
        aniCom.SetBool("isRunning", (agent.velocity.magnitude > 2.5f));
    }
    private void OnEnable()
    {
        PlayerController.onInteraction += CatchThief;
    }
    private void OnDisable()
    {
        PlayerController.onInteraction -= CatchThief;
    }
    private void GoToNextPoint()
    {
        if (aiWaypoints.Length == 0)
            return;

        agent.destination = aiWaypoints[currentWaypointIndex].transform.position;

        currentWaypointIndex = (currentWaypointIndex + 1) % aiWaypoints.Length;
    }
    private void CatchThief()
    {
        if (!interactable)
            return;

        agent.isStopped = true;
        GameManager.Instance.GameComplete();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.player.GetComponent<PlayerController>().canSeeInvisible)
            return;

        if(other.CompareTag("Player"))
        {
            GameUIManager.Instance.ToggleInteractObject(true);
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!GameManager.Instance.player.GetComponent<PlayerController>().canSeeInvisible)
            return;

        if (other.CompareTag("Player"))
        {
            GameUIManager.Instance.ToggleInteractObject(false);
            interactable = false;
        }
    }
}

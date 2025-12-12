using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GhostAI : MonoBehaviour
{
    [Header("Navigation")]
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    
    [Header("Settings")]
    public float chaseDuration = 3f;
    private float chaseTimer;
    
    private NavMeshAgent agent;
    private Transform player;
    private bool isChasing = false;
    private Animator animator;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Walking", true);
        GoToNextWaypoint();
    }

    void Update()
    {
        if (isChasing) {
            if (player != null) {
                agent.SetDestination(player.position);
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance < 0.8f)
                    GameManager.instance.lose();
            }
        }
        else {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint() {
        if (waypoints.Length == 0) return;
        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
            isChasing = true;
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
            Invoke("StopChasing", chaseDuration);
    }

    void StopChasing() {
        isChasing = false;
        player = null;
        GoToNextWaypoint();
    }
    
    public void AlertAndChase(Vector3 targetPosition) {
        agent.SetDestination(targetPosition);
        isChasing = true;
        CancelInvoke("StopChasing"); 
        Invoke("StopChasing", 5f);
    }
    
    public void ResetGhost()
    {
        isChasing = false;
        player = null;
        if (waypoints.Length > 0)
        {
            agent.Warp(waypoints[0].position);
            currentWaypointIndex = 0;
            agent.SetDestination(waypoints[0].position);
        }
    }
}
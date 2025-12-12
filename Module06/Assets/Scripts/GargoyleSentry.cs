using UnityEngine;

public class GargoyleSentry : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            AlertAllGhosts(other.transform.position);
    }

    void AlertAllGhosts(Vector3 playerPos) {
        GhostAI[] allGhosts = FindObjectsOfType<GhostAI>();

        foreach (GhostAI ghost in allGhosts)
        {
            ghost.AlertAndChase(playerPos);
        }
    }
}
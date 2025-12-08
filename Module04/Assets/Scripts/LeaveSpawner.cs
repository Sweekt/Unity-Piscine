using UnityEngine;

public class LeaveSpawner : MonoBehaviour
{
    [Header("Leave settings")]
    public GameObject leavePrefab;

    [Header("Spawn rate (Secondes)")]
    public float minDelay = 0.2f;
    public float maxDelay = 1.5f;

    [Header("Spawn Area")]
    public Vector2 spawnAreaSize = new Vector2(15f, 1f); 

    private float _timer;
    private float _currentDelay;

    void Start() {
        SetNextDelay();
    }

    void Update() {
        _timer += Time.deltaTime;
        if (_timer > _currentDelay) {
            SpawnLeaf();
            _timer = 0;
            SetNextDelay();
        }
    }

    void SetNextDelay() {
        _currentDelay = Random.Range(minDelay, maxDelay);
    }

    void SpawnLeaf() {
        Vector3 randomPosition = transform.position;
        randomPosition.x += Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        randomPosition.y += Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        GameObject newLeaf = Instantiate(leavePrefab, randomPosition, Quaternion.identity);
        newLeaf.transform.Rotate(0, 0, 70);
        Destroy(newLeaf, 5f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1));
    }
}
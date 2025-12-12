using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [Header("References")]
    public Transform Door;

    [Header("Settings")]
    public float openAngle = 90f;
    public float speed = 200f;
    public bool lockedDoor = false;
    private Quaternion _closedRotation; 
    private Quaternion _targetRotation;

    void Start()
    {
        _closedRotation = Door.localRotation;
        _targetRotation = _closedRotation;
    }

    void Update()
    {
        if (!Door) return;
        Door.localRotation = Quaternion.RotateTowards(
            Door.localRotation,
            _targetRotation,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lockedDoor && GameManager.instance.key < 3)
            return;
        
        if (other.CompareTag("Player")) {
            Vector3 directionToPlayer = other.transform.position - Door.position;
            float dotProduct = Vector3.Dot(Door.forward, directionToPlayer.normalized);

            float finalAngle = 0f;
            if (dotProduct > 0)
                finalAngle = openAngle;
            else
                finalAngle = -openAngle;
            _targetRotation = _closedRotation * Quaternion.Euler(0, finalAngle, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _targetRotation = _closedRotation;
    }
}
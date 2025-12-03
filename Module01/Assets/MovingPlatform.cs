using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float axisX = 1f;
    public float axisY = 1f;
    public float range = 1f;
    public float speed = 1f;
    bool forward = true;
    Vector3 defaultPosition;

    void Start() {
        defaultPosition = transform.localPosition;
        if (range < 0)
            range = Mathf.Abs(range);
    }
    
    void Update() {
        float targetX = defaultPosition.x;
        float targetY = defaultPosition.y;
        if (forward) {
            targetX += range * axisX;
            targetY += range * axisY;
        }
        float newX = Mathf.MoveTowards(transform.localPosition.x,targetX, speed * Time.deltaTime);
        float newY = Mathf.MoveTowards(transform.localPosition.y,targetY, speed * Time.deltaTime);
        Vector3 newPos = transform.localPosition;
        newPos.x = newX;
        newPos.y = newY;
        transform.localPosition = newPos;
        if (transform.localPosition.x == targetX && transform.localPosition.y == targetY)
            forward = !forward;
    }
}
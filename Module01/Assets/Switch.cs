using UnityEngine;

public class Switch : MonoBehaviour
{
    public SwitchHitbox Hitbox;
    public string triggerName = "Any";
    bool Trigger;
    float defaultY;
    public float speed = 0.5f;
    public float distance = 0.02f;

    void Start() {
        defaultY = transform.localPosition.y;
    }

    void Update() {
        if (triggerName == "Any")
            Trigger = Hitbox.Any;
        else if (triggerName == "Blue")
            Trigger = Hitbox.Blue;
        else if (triggerName == "Red")
            Trigger = Hitbox.Red;
        else if (triggerName == "Yellow")
            Trigger = Hitbox.Yellow;
        float targetY = defaultY; 
        if (Trigger)
            targetY -= distance;
        float newY = Mathf.MoveTowards(transform.localPosition.y, targetY, speed * Time.deltaTime);
        Vector3 newPos = transform.localPosition;
        newPos.y = newY;
        transform.localPosition = newPos;
    }
}
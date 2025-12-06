using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 0.5f;
    public float damages = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (target)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        else
            Destroy(this.gameObject);
    }
}
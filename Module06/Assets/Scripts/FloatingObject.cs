using System;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 50f;
    public Vector3 rotationAxis = new Vector3(0, 1, 0); 
    public float bobbingSpeed = 2f;
    public float bobbingAmount = 0.1f;
    private bool _taken;
    private Vector3 startPos;

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        transform.Rotate(rotationSpeed * Time.deltaTime * rotationAxis);
        
        float newY = startPos.y + (Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_taken) return;
        if (other.CompareTag("Player")) {
            _taken = true;
            GameManager.instance.getKey();
            GetComponent<Renderer>().enabled = false;
        }
    }

    public void RespawnKey()
    {
        _taken = false;
        GetComponent<Renderer>().enabled = true;
    }
}
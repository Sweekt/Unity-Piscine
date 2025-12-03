using UnityEngine;

public class SwitchHitbox : MonoBehaviour
{
    public bool Red;
    public bool Blue;
    public bool Yellow;
    public bool Any;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerRed"))
            Red = true;
        if (other.CompareTag("PlayerBlue"))
            Blue = true;
        if (other.CompareTag("PlayerYellow"))
            Yellow = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerRed"))
            Red = false;
        if (other.CompareTag("PlayerBlue"))
            Blue = false;
        if (other.CompareTag("PlayerYellow"))
            Yellow = false;
    }
    
    void Update() {
        if (Blue || Red || Yellow)
            Any = true;
        else
            Any = false;
    }
}
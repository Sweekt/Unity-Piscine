using TMPro;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float speed = 0.5f;
    private TextMeshProUGUI textMesh;

    void Start() {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        float hue = Mathf.Repeat(Time.time * speed, 1f);
        textMesh.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}

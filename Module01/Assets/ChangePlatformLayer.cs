using UnityEngine;

public class ChangePlatformLayer : MonoBehaviour
{
    public SwitchHitbox Hitbox;

    [Header("Couleurs")]
    public Color colorRed = Color.red;
    public Color colorBlue = Color.blue;
    public Color colorYellow = Color.yellow;
    public Color colorDefault = Color.white;

    // Variable privée pour stocker le Renderer
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Hitbox.Red) {
            gameObject.layer = LayerMask.NameToLayer("PlatformRed");
            _renderer.material.color = colorRed;
        }
        else if (Hitbox.Blue) {
            gameObject.layer = LayerMask.NameToLayer("PlatformBlue");
            _renderer.material.color = colorBlue;
        }
        else if (Hitbox.Yellow) {
            gameObject.layer = LayerMask.NameToLayer("PlatformYellow");
            _renderer.material.color = colorYellow;
        }
    }
}
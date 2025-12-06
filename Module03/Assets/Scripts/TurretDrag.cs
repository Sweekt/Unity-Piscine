using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TurretDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Réglages Raycast")]
    public LayerMask tileLayer;

    [Header("Stats")]
    public GameObject turretPrefab;
    public int cost = 5;
    public float cooldown = 5;

    [Header("Références UI")]
    public Canvas mainCanvas;
    private Image sourceImage;
    private GameObject dragGhost;
    private RectTransform ghostRect;
    private Color defaultColor;

    void Awake() {
        sourceImage = GetComponent<Image>();
        defaultColor = sourceImage.color;
        if (mainCanvas == null) mainCanvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (!GameManager.Instance.CanAfford(cost)) {
            Color c = sourceImage.color;
            c.g = 0f;
            c.b = 0f;
            sourceImage.color = c;
        } else
            sourceImage.color = defaultColor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.CanAfford(cost)) {
            eventData.pointerDrag = null;
            return;
        }
        dragGhost = new GameObject("IconGhost");
        dragGhost.transform.SetParent(mainCanvas.transform, false);
        Image ghostImage = dragGhost.AddComponent<Image>();
        ghostImage.sprite = sourceImage.sprite;
        ghostImage.raycastTarget = false;
        Color c = sourceImage.color;
        c.a = 0.6f;
        ghostImage.color = c;
        ghostRect = dragGhost.GetComponent<RectTransform>();
        ghostRect.sizeDelta = GetComponent<RectTransform>().sizeDelta;
        ghostRect.position = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragGhost != null) {
            ghostRect.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, tileLayer);
        if (hit.collider != null)
        {
            TileSpot spot = hit.collider.GetComponent<TileSpot>();
            if (spot != null && spot.isEmpty) {
                spot.PlaceTurret(turretPrefab);
                GameManager.Instance.SpendEnergy(cost);
            }
        }
        if (dragGhost != null) {
            Destroy(dragGhost);
        }
    }
}
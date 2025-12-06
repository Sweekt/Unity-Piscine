using UnityEngine;

public class TileSpot : MonoBehaviour
{
    public bool isEmpty = true;

    public void PlaceTurret(GameObject turretToSpawn)
    {
        // Instancie la tourelle sur la position de la case
        Instantiate(turretToSpawn, transform.position, Quaternion.identity);
        isEmpty = false;
        
        // Optionnel : Changer la couleur de la case pour montrer qu'elle est occupée
    }
}

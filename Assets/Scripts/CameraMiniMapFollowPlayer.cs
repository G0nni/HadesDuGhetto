using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiniMapFollowPlayer : MonoBehaviour
{
    // Variables
    [Header("Player")]
    public GameObject player;
    private Vector3 offset;

    
    void Start()
    {
        // Calcul de la distance entre la cam�ra et le joueur
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        // Mise � jour de la position de la cam�ra
        transform.position = player.transform.position + offset;
    }
}

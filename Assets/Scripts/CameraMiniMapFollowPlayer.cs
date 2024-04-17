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
        // Calcul de la distance entre la caméra et le joueur
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        // Mise à jour de la position de la caméra
        transform.position = player.transform.position + offset;
    }
}

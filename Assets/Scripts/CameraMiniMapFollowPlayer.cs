using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiniMapFollowPlayer : MonoBehaviour
{
    public GameObject player; // Référence au GameObject du joueur

    private Vector3 offset; // Offset entre la caméra et le joueur

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}

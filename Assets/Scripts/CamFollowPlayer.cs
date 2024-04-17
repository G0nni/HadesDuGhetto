using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    // Variables
    [Header("Cam�ra")]
    public Transform target;
    public Vector3 offset;

    void Update()
    {
        // Update camera position
        transform.position = target.position+ offset;
    }
}

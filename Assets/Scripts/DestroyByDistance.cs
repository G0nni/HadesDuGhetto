using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByDistance : MonoBehaviour
{
    void onTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}

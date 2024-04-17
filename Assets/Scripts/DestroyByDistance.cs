using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByDistance : MonoBehaviour
{
    // D�truit les objets qui sortent de la zone de jeu
    void onTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIndicator : MonoBehaviour
{
    // Variables
    [Header("Duration")]
    public float displayDuration = 1.0f;

    // M�thode pour afficher le cercle � une position donn�e
    public void Show(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;

        // D�sactivez le cercle apr�s un certain d�lai
        Invoke("Hide", displayDuration);
    }

    // M�thode pour cacher le cercle
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

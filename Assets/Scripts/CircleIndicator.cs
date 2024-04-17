using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIndicator : MonoBehaviour
{
    // Variables
    [Header("Duration")]
    public float displayDuration = 1.0f;

    // Méthode pour afficher le cercle à une position donnée
    public void Show(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;

        // Désactivez le cercle après un certain délai
        Invoke("Hide", displayDuration);
    }

    // Méthode pour cacher le cercle
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

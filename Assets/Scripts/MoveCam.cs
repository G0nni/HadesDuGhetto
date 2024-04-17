using UnityEngine;

public class MoveCam : MonoBehaviour
{
    // Variables
    [Header("Vitesse")]
    public float speed = 10.0f;

    void Update()
    {
        // Déplacement de la caméra
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
    }
}

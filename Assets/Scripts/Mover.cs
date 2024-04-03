using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 10;
    public Transform playerTransform;

    private Quaternion initialRotation;

    private bool directionSet = false;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialRotation = playerTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!directionSet)
        {

            transform.LookAt(playerTransform.position + playerTransform.forward);
            directionSet = true;
        }
        transform.rotation = initialRotation;
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

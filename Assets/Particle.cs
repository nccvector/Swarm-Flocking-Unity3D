using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float speed = 20f;
    public float randomness = 0.1f;
    public float biasness = 0.5f;
    public float repulsionRadius = 1f;

    Rigidbody rb;
    Vector3 random_direction;
    List<Collider> colliders;
    Vector3 steer;
    Vector3 direction;
    Vector3 affectDirection;
    Quaternion rotation;
    Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        random_direction = new Vector3(0f,0f,0f);
        colliders = new List<Collider>();
        target = GameObject.Find("target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Always move in the heading direction
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);

        random_direction += new Vector3(
            Random.Range(-randomness, randomness),
            Random.Range(-randomness, randomness),
            Random.Range(-randomness, randomness));

        random_direction = Vector3.Normalize(random_direction);

        // Getting a baised steering vector
        steer = Vector3.Normalize(target.position - transform.position); // Towards origin

        direction = Vector3.Normalize(biasness * steer + (1f-biasness) * random_direction);

        affectDirection = Vector3.zero;
        
        int itt = colliders.Count;
        if(itt > 5)
        {
            itt = 5;
        }

        // Runnning loop only till itt count
        for(int i=0; i<itt; i++)
        {
            if(Vector3.Distance(transform.position, colliders[i].transform.position) < repulsionRadius)
            {
                affectDirection += Vector3.Normalize(transform.position - colliders[i].transform.position); // Opposite direction of other
            }
            else
            {
                affectDirection += colliders[i].transform.forward; // Heading direction of other
            }
        }

        affectDirection = Vector3.Normalize(affectDirection);

        // Calculating rotation from random vector
        rotation = Quaternion.LookRotation(0.4f * transform.forward + 0.5f * affectDirection + 0.1f * direction, transform.up);
        rb.MoveRotation(rotation);
    }

    private void OnTriggerEnter (Collider other) {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);
    }
}

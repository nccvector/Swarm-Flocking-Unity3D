using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pref;
    public int numParticles = 100;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for(int i=0; i<numParticles; i++)
        {
            Instantiate(pref, Vector3.zero, Quaternion.identity);
            yield return null;
        }
    }
}

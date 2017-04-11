using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{

    public float maxGravDist = 4.0f;
    public float maxGravity = 35.0f;

    GameObject[] planets;

    Rigidbody2D rigid;

    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;

        foreach (var planet in planets)
        {
            var planetCenter = planet.transform.position;
            float dist = Vector3.Distance(planetCenter, position);

            if (dist <= maxGravDist)
            {
                Vector3 v = planetCenter - position;
                var gravity = 1.0f - dist / maxGravDist;

                rigid.AddForce(v.normalized * gravity * maxGravity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
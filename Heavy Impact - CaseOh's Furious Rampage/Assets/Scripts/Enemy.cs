using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 lateTransformPosition = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = this.transform.position - lateTransformPosition;
        sr.flipX = velocity.x > 0;
        lateTransformPosition = this.transform.position;
    }
}

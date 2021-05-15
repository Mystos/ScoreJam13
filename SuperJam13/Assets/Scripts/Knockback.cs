using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public Rigidbody2D rb;
    public float knockbackForce;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Knock()
    {
      rb.AddForce(transform.right * knockbackForce);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    float mass = 3.0f;
    Vector3 impact = Vector3.zero;
    private Controller2D characterController;


    private void Start()
    {
        characterController = GetComponent<Controller2D>();
    }

    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if(dir.y < 0)
        {
            dir.y = -dir.y;
        }
        impact += dir.normalized * force / mass;

    }

    private void Update()
    {
        if(impact.magnitude > 0.2f)
        {
           characterController.Move(impact * Time.deltaTime);
        }

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

}

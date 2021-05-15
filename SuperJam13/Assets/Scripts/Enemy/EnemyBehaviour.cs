using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    private AIDestinationSetter agent;
    private Transform target;
    public float damping = 0.1f;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    public float offset;

    private void Start()
    {
        agent = GetComponent<AIDestinationSetter>();
        agent.target = FindObjectOfType<PlayerController>()?.gameObject.transform;
        target = agent.target;
        //controller = gameObject.GetComponent<CharacterController>();
    }

    public void Update()
    {
        if(target != null)
        {
            targetPos = target.position;
            thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        }
    }
}
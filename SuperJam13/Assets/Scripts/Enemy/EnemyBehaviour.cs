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
    public LayerMask detectionLayerMask;
    public float detectionRadius = 5.5f;
    internal Transform spawnLocation;
    public bool alwaysSeePlayer = false;

    private void Start()
    {
        agent = GetComponent<AIDestinationSetter>();
        agent.target = null;
        target = null;
        //controller = gameObject.GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (!alwaysSeePlayer)
        {
            Collider2D player = Physics2D.OverlapCircle(new Vector2(this.transform.position.x, this.transform.position.y), detectionRadius, detectionLayerMask);
            if (player != null)
            {
                target = player.transform;
                agent.target = player.transform;
            }
            else
            {
                target = null;
                agent.target = null;
            }
        }
        else
        {
            target = FindObjectOfType<PlayerController>()?.transform;
            agent.target = target.transform;
        }



        if (target != null)
        {
            targetPos = target.position;
            thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(this.transform.position, detectionRadius);
    }
}

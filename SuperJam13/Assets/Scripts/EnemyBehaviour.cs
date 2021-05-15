using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    private AIDestinationSetter agent;

    private void Start()
    {
        agent = GetComponent<AIDestinationSetter>();
        agent.target = FindObjectOfType<PlayerController>()?.gameObject.transform;
        //controller = gameObject.GetComponent<CharacterController>();
    }
}
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null && agent.enabled)
        {
            Debug.Log(gameObject.name + " is chasing, agent on navmesh: " + agent.isOnNavMesh);
            if (agent.isOnNavMesh)
                agent.SetDestination(player.position);
            else
                Debug.Log(gameObject.name + " is NOT on NavMesh!");
        }
    }
}
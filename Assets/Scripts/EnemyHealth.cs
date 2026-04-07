using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHits = 1;
    private int currentHits = 0;

    public void TakeHit() //don't really need it now
    {
        currentHits++;
        Debug.Log(gameObject.name + " hit! Hits taken: " + currentHits);

        if (currentHits >= maxHits)
        {
            Debug.Log(gameObject.name + " is dead!");
            Destroy(gameObject);
        }
    }
    public void InstantDeath()
    {
        Destroy(gameObject);
    }

    public void SlowEnemy(float duration)
    {
        StartCoroutine(SlowCoroutine(duration));
    }

    IEnumerator SlowCoroutine(float duration)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        float originalSpeed = agent.speed;
        agent.speed = originalSpeed * 0.3f; // slow to 30%
        yield return new WaitForSeconds(duration);
        agent.speed = originalSpeed;
    }

    public void SpinAndSlow(float duration)
    {
        StartCoroutine(SpinCoroutine(duration));
    }

    IEnumerator SpinCoroutine(float duration)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        float originalSpeed = agent.speed;
        agent.speed = originalSpeed * 0.3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.Rotate(0, 360 * Time.deltaTime, 0);
            elapsed += Time.deltaTime;
        }
        agent.speed = originalSpeed;
        yield break;
    }
}
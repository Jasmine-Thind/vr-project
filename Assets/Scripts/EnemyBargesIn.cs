using System.Collections;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    public GameObject enemy;          // drag your enemy object here
    public Animator doorAnimator;     // drag the door's Animator here
    public float timerDuration = 180f; // 3 minutes

    void Start()
    {
        StartCoroutine(EnemyEntrance());
    }

    IEnumerator EnemyEntrance()
    {
        yield return new WaitForSeconds(timerDuration);

        // 1. Play door barge animation
        if (doorAnimator != null)
            doorAnimator.SetTrigger("BargeTrigger");

        // 2. Activate and play enemy barge-in animation
        enemy.SetActive(true);
        Animator enemyAnim = enemy.GetComponent<Animator>();
        if (enemyAnim != null)
            enemyAnim.SetTrigger("StartBarge");
    }
}
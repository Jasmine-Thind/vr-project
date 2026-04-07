using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHits = 1;
    private int currentHits = 0;

    public void TakeHit()
    {
        currentHits++;
        Debug.Log(gameObject.name + " hit! Hits taken: " + currentHits);

        if (currentHits >= maxHits)
        {
            Debug.Log(gameObject.name + " is dead!");
            Destroy(gameObject);
        }
    }
}
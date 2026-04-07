using UnityEngine;

public class Vial : MonoBehaviour
{
    //vial needs to have a rigidbody component!
    //A Rigidbody so it can be thrown
    //A Collider(not Is Trigger)
    //Your Vial.cs script attached
    void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeHit();
            Destroy(gameObject); // vial breaks on impact
        }
    }
}
using System.Collections;
using UnityEngine;

public class EnemyBargesIn : MonoBehaviour
{
    public float timerDuration = 10f;
    public Transform endPosition;
    public float moveSpeed = 3f;
    public GameObject door;
    public float doorOpenAngle = -90f;
    public float doorOpenSpeed = 2f;

    private bool moving = false;
    private bool openDoor = false;
    private Quaternion targetRotation;

    void Start()
    {
        StartCoroutine(EnemyEntrance());
    }

    IEnumerator EnemyEntrance()
    {
        yield return new WaitForSeconds(timerDuration);
        targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        openDoor = true;
        yield return new WaitForSeconds(1f);
        moving = true;
    }

    void Update()
    {
        if (openDoor)
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetRotation, doorOpenSpeed * Time.deltaTime);

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, moveSpeed * Time.deltaTime);
            if (transform.position == endPosition.position)
                moving = false;
        }
    }
}
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
    private bool closeDoor = false;
    private Quaternion targetRotation;
    private Quaternion targetCloseRotation;

    void Start()
    {
        targetCloseRotation = door.transform.rotation;
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
            {
                moving = false;
                openDoor = false;
                closeDoor = true;
                StartCoroutine(DieAfterDelay());
            }            
        }
        if (closeDoor)
        {
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetCloseRotation, doorOpenSpeed * Time.deltaTime);
        }
    }
    IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
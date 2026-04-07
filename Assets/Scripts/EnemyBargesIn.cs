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

    private bool movingToEndPoint = false;
    private bool chasingPlayer = false;
    private bool openDoor = false;
    private bool closeDoor = false;
    private Quaternion targetOpenRotation;
    private Quaternion targetCloseRotation;
    private Transform player;

    void Start()
    {
        targetCloseRotation = door.transform.rotation;
        StartCoroutine(EnemyEntrance());
    }

    IEnumerator EnemyEntrance()
    {
        yield return new WaitForSeconds(timerDuration);
        targetOpenRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        openDoor = true;
        yield return new WaitForSeconds(1f);
        movingToEndPoint = true;
    }

    void Update()
    {
        if (openDoor)
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetOpenRotation, doorOpenSpeed * Time.deltaTime);

        if (movingToEndPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, moveSpeed * Time.deltaTime);

            if (transform.position == endPosition.position)
            {
                movingToEndPoint = false;
                openDoor = false;
                closeDoor = true;
                chasingPlayer = true; // start chasing after reaching endpoint
            }
        }

        if (closeDoor)
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetCloseRotation, doorOpenSpeed * Time.deltaTime);

        if (chasingPlayer)
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
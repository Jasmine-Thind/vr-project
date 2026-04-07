using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBargesIn : MonoBehaviour
{
    public float timerDuration = 10f;
    public Transform endPosition;
    public float moveSpeed = 3f;
    public GameObject door;
    public float doorOpenAngle = -90f;
    public float doorOpenSpeed = 2f;

    private bool movingToEndPoint = false;
    private bool openDoor = false;
    private bool closeDoor = false;
    private Quaternion targetOpenRotation;
    private Quaternion targetCloseRotation;
    private NavMeshAgent agent;
    private EnemyChase enemyChase;

    void Start()
    {
        targetCloseRotation = door.transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        enemyChase = GetComponent<EnemyChase>();
        agent.enabled = false;
        enemyChase.enabled = false; // disable chase at start
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

            if (Vector3.Distance(transform.position, endPosition.position) < 0.1f)
            {
                movingToEndPoint = false;
                openDoor = false;
                closeDoor = true;
                agent.enabled = true;
                enemyChase.enabled = true; // start chasing now
            }
        }

        if (closeDoor)
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetCloseRotation, doorOpenSpeed * Time.deltaTime);
    }
}
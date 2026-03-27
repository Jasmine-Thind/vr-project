using UnityEngine;

public class PourPotion : MonoBehaviour
{
    public Transform spout;
    public float maxTilt = 45f;
    private bool isSpilling = false;

    void Update()
    {
        float tiltAngle = Vector3.Angle(Vector3.up, transform.up);

        if (tiltAngle > maxTilt)
        {
            if (!isSpilling) StartPour();
        }
        else
        {
            if (isSpilling) StopPour();
        }
    }

    void StartPour()
    {
        isSpilling = true;
        Debug.Log("start pour");
    }

    void StopPour()
    {
        isSpilling = false;
        Debug.Log("end pour");
    }
}
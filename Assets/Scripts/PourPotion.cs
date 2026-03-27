using UnityEngine;

public class PourPotion : MonoBehaviour
{
    public Transform spout;
    public float maxTilt = 45f;
    private bool isSpilling = false;
    public ParticleSystem pourParticles;

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

        Transform fluidTransform = transform.Find("Fluid");

        if (fluidTransform != null)
        {

            Color potionColor = fluidTransform.GetComponentInChildren<MeshRenderer>().material.GetColor("_Tint");

            ParticleSystem.MainModule main = pourParticles.main;
            main.startColor = potionColor;

            ParticleSystem.TrailModule trails = pourParticles.trails;
            trails.colorOverTrail = new ParticleSystem.MinMaxGradient(potionColor);
        }

        pourParticles.Play();
        Debug.Log("start pour");
    }

    void StopPour()
    {
        isSpilling = false;
        pourParticles.Stop();
        Debug.Log("end pour");
    }
}
using UnityEngine;

public class PourPotion : MonoBehaviour
{
    public Transform spout;
    public float maxTilt = 45f;
    private bool isSpilling = false;
    public ParticleSystem pourParticles;
    public MeshRenderer liquidRenderer;
    public float drainSpeed = 0.05f;

    private float tiltAngle = 0;

    public Liquid liquidScript;

    private bool pouredFully = false;

    void Update()
    {
        tiltAngle = Vector3.Angle(Vector3.up, transform.up);

        if (tiltAngle > maxTilt)
        {
            if (!isSpilling && !pouredFully)
            {
                StartPour();
                
            }
            if (isSpilling)
            {
                UpdateFillLevel();
            }
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

            Color potionColor = fluidTransform.GetComponentInChildren<MeshRenderer>().material.GetColor("_TopColor");

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

    void UpdateFillLevel()
    {

        // The logic in the script I got from MinionsArt seems to be inverted
        // So the fill amount of 0 makes it full
        float tiltInfluence = Mathf.InverseLerp(maxTilt, 90f, tiltAngle);
        liquidScript.fillAmount += drainSpeed * tiltInfluence * Time.deltaTime;
        liquidScript.fillAmount = Mathf.Clamp(liquidScript.fillAmount, 0f, 1f);

        if (liquidScript.fillAmount >= 1.0f)
        {
            pourParticles.Stop();
            pouredFully = true;
        }
    }
}
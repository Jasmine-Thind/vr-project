using UnityEngine;

public class PotionCollision : MonoBehaviour
{
    public Liquid liquidScript;
    public float fillSpeed = 0.5f;
    public float colorMixSpeed = 2.0f;

    private Color originalColor;
    private Color targetColor;
    private bool hasCalculatedTarget = false;

    void Start()
    {
        originalColor = liquidScript.GetComponent<MeshRenderer>().material.GetColor("_Tint");
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.IsChildOf(transform)) return;

        Debug.Log(gameObject.name + " is being poured into by " + other.name);

        ParticleSystem part = other.GetComponent<ParticleSystem>();
        Color incomingColor = part.main.startColor.color;

        if (!hasCalculatedTarget)
        {
            targetColor = Color.Lerp(originalColor, incomingColor, 0.5f);
            hasCalculatedTarget = true;
        }

        liquidScript.fillAmount -= fillSpeed * Time.deltaTime;
        liquidScript.fillAmount = Mathf.Clamp(liquidScript.fillAmount, 0f, 1f);

        MeshRenderer rend = liquidScript.GetComponent<MeshRenderer>();
        Color currentColor = rend.material.GetColor("_Tint");

        Color newColor = Color.Lerp(currentColor, targetColor, colorMixSpeed * Time.deltaTime);

        rend.material.SetColor("_TopColor", newColor);
        rend.material.SetColor("_Tint", newColor);
    }
}
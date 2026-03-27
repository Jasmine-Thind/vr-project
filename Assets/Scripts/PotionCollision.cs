using UnityEngine;

public class PotionCollision : MonoBehaviour
{
    public Liquid liquidScript;
    public Color mixColor = new Color(1f, 0.5f, 0f);
    public float fillSpeed = 0.1f;
    public float colorMixSpeed = 0.5f;

    private void OnParticleCollision(GameObject other)
    {
        // TODO: Continue debugging why this doesn't work
        Debug.Log("collided!");
        liquidScript.fillAmount -= fillSpeed * Time.deltaTime;
        liquidScript.fillAmount = Mathf.Clamp(liquidScript.fillAmount, 0f, 1f);

        Color currentColor = liquidScript.GetComponent<MeshRenderer>().material.GetColor("_TopColor");
        Color newColor = Color.Lerp(currentColor, mixColor, colorMixSpeed * Time.deltaTime);

        liquidScript.GetComponent<MeshRenderer>().material.SetColor("_TopColor", newColor);
        liquidScript.GetComponent<MeshRenderer>().material.SetColor("_Tint", newColor);
    }
}

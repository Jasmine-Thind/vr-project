using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    public Liquid liquidScript;
    public ParticleSystem pourParticles;

    [Header("Recipe Colors")]
    public Color slowColor = new Color(1f, 0f, 0f); // Red
    public Color greenColor = new Color(0f, 1f, 0f); // Green
    public Color mixColor = new Color(1f, 0.5f, 0f); // Orange

    [Header("Settings")]
    public float colorThreshold = 1f;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("COLLIDED WITH SOMETHING");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("COLLIDED!!!!!!!!!!!!");
            ApplyEffect(collision.gameObject);
            BreakBottle();
        }
    }

    void ApplyEffect(GameObject enemy)
    {
        Color currentColor = liquidScript.GetComponent<MeshRenderer>().material.GetColor("_Tint");

        if (IsColorInRange(currentColor, slowColor))
        {
            TriggerSlow(enemy);
        }
        else if (IsColorInRange(currentColor, greenColor))
        {
            TriggerGreen(enemy);
        }
        /*else if (IsColorInRange(currentColor, mixColor))
        {
            
        }*/
        else
        {
            TriggerMix(enemy);
        }
    }

    bool IsColorInRange(Color current, Color target)
    {
        Debug.Log("current colour is");
        Debug.Log(current.ToString());
        float distance = Mathf.Abs(current.r - target.r) +
                         Mathf.Abs(current.g - target.g) +
                         Mathf.Abs(current.b - target.b);

        if (distance < colorThreshold)
        {
            Debug.Log("colour" + current.ToString() + " is " + target.ToString());
        }

        return distance < colorThreshold;
    }

    void BreakBottle()
    {
        Debug.Log("breaking bottle");
        Color finalColor = liquidScript.GetComponent<MeshRenderer>().material.GetColor("_Tint");

        if (pourParticles != null)
        {
            pourParticles.transform.parent = null;

            var main = pourParticles.main;
            main.startColor = finalColor;

            pourParticles.Emit(50);

            Destroy(pourParticles.gameObject, main.duration);
        }

        Destroy(gameObject);
    }

    void TriggerSlow(GameObject enemy) { Debug.Log("slowed"); }
    void TriggerGreen(GameObject enemy) { Debug.Log("green"); }
    void TriggerMix(GameObject enemy) { Debug.Log("mix"); }
}
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject timerCanvas;

    private float[] attackTimes = { 10f, 20f }; // 3 mins for spider, 1 min for rat //use { 5f, 10f }; to test
    private int currentAttack = 0;
    private float timeRemaining;

    void Start()
    {
        timeRemaining = attackTimes[currentAttack];
    }

    void Update()
    {
        if (currentAttack >= attackTimes.Length)
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentAttack++;
            if (currentAttack >= attackTimes.Length)
            {
                timerCanvas.SetActive(false); // hide timer after last enemy
                return;
            }
            timeRemaining = attackTimes[currentAttack];
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = "Next attack in: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeRemaining <= 5)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;
    }
}
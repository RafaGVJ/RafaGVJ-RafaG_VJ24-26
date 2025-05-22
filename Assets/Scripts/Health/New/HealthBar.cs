using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image totalHealthBar;

    [SerializeField] private Health playerHealth;


    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
            totalHealthBar.fillAmount = 1f;
        }
    }

    private void Update()
    {
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
                totalHealthBar.fillAmount = 1f;
            }
            return; // ainda sem player
        }
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }
}








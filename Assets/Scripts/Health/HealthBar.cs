using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<Health>();
        }

        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.StartingHealth);
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    public void SetTarget(Health newHealth)
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateHealthBar;

        playerHealth = newHealth;

        if (playerHealth != null)
            playerHealth.OnHealthChanged += UpdateHealthBar;

        UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.StartingHealth);
    }

    private void UpdateHealthBar(float current, float total)
    {
        currentHealthBar.fillAmount = current / total;
        totalHealthBar.fillAmount = 1f; // total é sempre cheio
    }
}

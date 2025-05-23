using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image totalHealthBar;

    private PlayerHealth playerHealth;


    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged += UpdateHealthUI;
                UpdateHealthUI(playerHealth.CurrentHealth, playerHealth.MaxHealth);

                if (totalHealthBar != null)
                    totalHealthBar.fillAmount = 1f;
            }
            else
            {
                Debug.LogError("PlayerHealth component não encontrado no objeto Player.");
            }
        }
        else
        {
            Debug.LogError("Objeto com tag 'Player' não encontrado na cena.");
        }
    }



    private void UpdateHealthUI(int current, int max)
    {
        currentHealthBar.fillAmount = (float)current / max;
    }
}









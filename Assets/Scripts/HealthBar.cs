using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHeath;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;

    private void Start()
    {
        currenthealthbar.fillAmount = playerHeath.currentHealth / 10;
    }

    private void Update()
    {
        currenthealthbar.fillAmount = playerHeath.currentHealth / 10;
    }
    
}

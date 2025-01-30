using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Actor : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    public bool isPlayer = false;

    TMP_Text hpText;

    void Awake() {
        currentHealth = maxHealth;
        hpText = GetComponent<TMP_Text>();
        hpText.text = currentHealth.ToString();
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        hpText.text = currentHealth.ToString();
        if (currentHealth <=0 ){
            Death();
        }
    }

    void Death() {
        Destroy(gameObject);
        if (isPlayer) {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}

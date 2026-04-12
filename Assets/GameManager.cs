using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int appleCount = 0;
    public int vidas = 3;
    public TMP_Text appleText;
    public TMP_Text vidasText;
    public AudioClip collectSound;
    private AudioSource audioSource;
    public static GameManager instance;
    public bool venceu = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && collectSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        UpdateAppleText();
    }

    public void CollectApple()
    {
        appleCount++;
        Debug.Log("Maçã coletada! Total: " + appleCount);
        UpdateAppleText();
        
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }

    void UpdateAppleText()
    {
        if (appleText != null)
        {
            appleText.text = "🍎: " + appleCount;
        }
    }

    public void PlayerAtingido()
    {
        vidas--;
        vidasText.text = "Vidas: " + vidas;

        if (vidas <= 0)
        {
            venceu = false;
            // Resetar time scale antes de mudar de cena
            Time.timeScale = 1f;
            SceneManager.LoadScene("telainicial");
        }
    }
    public void AtualizarUI()
    {
        
        if (vidasText  != null) vidasText.text  = "Vidas: "  + vidas;
    }
    
}
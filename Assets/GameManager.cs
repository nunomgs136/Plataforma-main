using UnityEngine;
using TMPro; // <-- Adicione isso

public class GameManager : MonoBehaviour
{
    public int appleCount = 0;
    public TMP_Text appleText; // <-- Mude de Text para TMP_Text
    public AudioClip collectSound;
    private AudioSource audioSource;
    public static GameManager instance;
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
}
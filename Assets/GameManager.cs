using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int appleCount = 0;
    public int vidas = 3;
    public int pontos = 0;
    public int macasRestantes = 0;
    public TMP_Text appleText;
    public TMP_Text vidasText;
    public AudioClip collectSound;
    private AudioSource audioSource;
    public static GameManager instance;
    public bool venceu = false;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Registra o método para ser chamado toda vez que uma cena carregar
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        macasRestantes = FindObjectsOfType<Apple>().Length;
        // Só busca os textos na cena do jogo, não na TelaFinal
        if (scene.name == "fase1" || scene.name == "fase2")
        {
            appleText = GameObject.Find("AppleText")?.GetComponent<TMP_Text>();
            vidasText  = GameObject.Find("VidasText")?.GetComponent<TMP_Text>();
            AtualizarUI();
        }
    }
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
        macasRestantes--;
        Debug.Log("Maçã coletada! Total: " + appleCount);
        UpdateAppleText();
        
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
        if (macasRestantes == 0){
            ProximaFase();
        }
    }
    void ProximaFase()
{
    string cenaAtual = SceneManager.GetActiveScene().name;

    if (cenaAtual == "fase1")
    {
        SceneManager.LoadScene("fase2");
    }
    else if (cenaAtual == "fase2")
    {
        venceu = true;
        SceneManager.LoadScene("telaFinal");
    }
}

    void UpdateAppleText()
    {
        if (appleText != null)
        {
            appleText.text = "Maçãs: " + appleCount;
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
            SceneManager.LoadScene("telaFinal");
        }
    }
    public void AtualizarUI()
    {
        if(appleText != null) appleText.text = "Maçãs: " + appleCount;
        if (vidasText  != null) vidasText.text  = "Vidas: "  + vidas;
    }
    
}
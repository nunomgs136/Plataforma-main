using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // <-- Adicione esta linha

public class TelaFinal : MonoBehaviour
{
    public TextMeshProUGUI mensagem;

    void Start()
    {
        if (mensagem == null)
        {
            Debug.LogError("❌ A referência 'mensagem' está nula! Atribua o Text (TMP) no Inspector.");
            return;
        }
        else
        {
            Debug.Log("✅ Texto conectado: " + mensagem.name);
        }

        if (GameManager.instance == null)
        {
            Debug.LogError("❌ GameManager.instance é nulo! Ele pode ter sido destruído.");
            return;
        }
        else
        {
            Debug.Log("✅ GameManager encontrado. venceu = " + GameManager.instance.venceu);
        }

        if (GameManager.instance.venceu)
        {
            mensagem.text = "VOCÊ VENCEU!";
            mensagem.color = Color.green;
        }
        else
        {
            mensagem.text = "VOCÊ PERDEU!";
            mensagem.color = Color.red;
        }
    }
public void ReiniciarJogo()
{
    // Reseta os valores antes de trocar de cena
    GameManager.instance.pontos = 0;
    GameManager.instance.vidas = 5;
    GameManager.instance.appleCount = 0;
    GameManager.instance.venceu = false;

    SceneManager.LoadScene("fase1");
}
}
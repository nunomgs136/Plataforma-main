using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidade = -5f;
    
    // Sistema de tiro
    public GameObject raioPrefab;     // Prefab do raio
    public float tempoTiro = 2f;      // Tempo entre tiros
    
    void Start()
    {
        // Começar a atirar em intervalos regulares
        InvokeRepeating("Atirar", 1f, tempoTiro);
    }
    
    void Update()
    {
        // Move o inimigo para baixo
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        // Destrói o inimigo ao sair da tela
        if (transform.position.y < -10f || transform.position.x < -15f || transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }
    
    void Atirar()
    {
        if (raioPrefab != null)
        {
            // Criar o tiro na posição do inimigo
            GameObject tiro = Instantiate(raioPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            
            // Configurar o raio como tiro inimigo
            Raio scriptRaio = tiro.GetComponent<Raio>();
            if (scriptRaio != null)
            {
                scriptRaio.direcao = Vector3.left;  // Tiro vai para baixo
                scriptRaio.tiroInimigo = true;      // Marcar como tiro inimigo
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Raio"))
        {
            Raio raioScript = other.GetComponent<Raio>();
            // Verificar se é tiro do jogador (não é tiro inimigo)
            if (raioScript != null && !raioScript.tiroInimigo)
            {
                //gameManager.instance.InimigoDestruido();
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Player"))
        {
            //gameManager.instance.PlayerAtingido();
            Destroy(gameObject);
        }
    }
}
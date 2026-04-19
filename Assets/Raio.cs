using UnityEngine;

public class Raio : MonoBehaviour
{
    public float velocidade = 15f;
    public Vector3 direcao = Vector3.right;
    public bool tiroInimigo = false;
    
    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
        
        // Destroi o raio após 5 segundos se não acertar nada
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Raio colidiu com: " + col.gameObject.name + " | Tag: " + col.gameObject.tag);
        
        // TIRO DO JOGADOR vs INIMIGO
        if (!tiroInimigo && col.CompareTag("Inimigo"))
        {
            Debug.Log("INIMIGO MORREU!");
            Destroy(col.gameObject);  // Destroi o inimigo
            Destroy(gameObject);       // Destroi o raio
            return;
        }

        // Destrói o raio ao bater na parede
        if (!tiroInimigo && col.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

    if (tiroInimigo && col.CompareTag("Player"))
    {
        GameManager.instance.PlayerAtingido();
        Destroy(gameObject);
    }
    }
}
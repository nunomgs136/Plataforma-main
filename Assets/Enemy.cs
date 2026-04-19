using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidade = -5f;
    public GameObject raioPrefab;
    public float tempoTiro = 2f;
    
    void Start()
    {
        InvokeRepeating("Atirar", 1f, tempoTiro);
    }
    
    void Update()
    {
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);

        if (transform.position.y < -10f || transform.position.x < -15f || transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }
    
    void Atirar()
    {
        if (raioPrefab != null)
        {
            GameObject tiro = Instantiate(raioPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            
            Raio scriptRaio = tiro.GetComponent<Raio>();
            if (scriptRaio != null)
            {
                scriptRaio.direcao = Vector3.left;  // Tiro vai para BAIXO
                scriptRaio.tiroInimigo = true;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Inimigo colidiu com: " + other.tag);
        
        if (other.CompareTag("Raio"))
        {
            Raio raioScript = other.GetComponent<Raio>();
            if (raioScript != null && !raioScript.tiroInimigo)
            {
                Debug.Log("Inimigo foi atingido pelo tiro do jogador!");
                Destroy(other.gameObject); // Destroi o raio
                Destroy(gameObject);        // Destroi o inimigo
            }
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Inimigo encostou no player!");
            GameManager.instance.PlayerAtingido();
            
        }
    }
}
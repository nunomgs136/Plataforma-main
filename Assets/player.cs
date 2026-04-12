using UnityEngine;
using TMPro;
public class player : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode shootKey = KeyCode.Space;

    public float velocidade = 10f;
    
    // Prefab do raio
    public GameObject raioPrefab;
    
    // Ponto de onde o raio sai
    public Transform pontoDisparo;
    
    // Paredes
    public Transform leftWall;
    public Transform rightWall;
    
    // Referência para o chão (opcional)
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    
    void Start()
    {
        // Pega o componente Rigidbody2D (recomendado para movimento)
        rb = GetComponent<Rigidbody2D>();
        
        // Se não tiver Rigidbody2D, adiciona um
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 3f; // Gravidade
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Congela rotação
        }
        
        // Se não tiver um groundCheck, cria um automaticamente
        if (groundCheck == null)
        {
            GameObject groundObj = new GameObject("GroundCheck");
            groundObj.transform.parent = transform;
            groundObj.transform.localPosition = new Vector3(0, -0.5f, 0);
            groundCheck = groundObj.transform;
        }
    }
    
    void Update()
    {
        // Movimento horizontal (apenas A e D)
        float movimento = 0f;
        
        if (Input.GetKey(moveLeft))
            movimento = -1f;
        
        if (Input.GetKey(moveRight))
            movimento = 1f;
        
        // Move o jogador horizontalmente
        Vector3 deslocamento = Vector3.right * movimento * velocidade * Time.deltaTime;
        transform.Translate(deslocamento);
        
        // Verifica se está no chão (opcional)
        VerificarChao();
        
        // Limites das paredes (apenas horizontal agora)
        if (leftWall != null && rightWall != null)
        {
            Vector3 posicaoAtual = transform.position;
            posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, leftWall.position.x, rightWall.position.x);
            transform.position = posicaoAtual;
        }
        
        // Atirar
        if (Input.GetKeyDown(shootKey))
        {
            Atirar();
        }
    }
    
    void VerificarChao()
    {
        // Verifica se está tocando o chão (se tiver groundLayer configurado)
        if (groundLayer != 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
            
            // Opcional: se quiser que o player só ande quando está no chão
            // Você pode descomentar a linha abaixo
            // if (!isGrounded) return;
        }
    }
    
    void Atirar()
    {
        if (raioPrefab != null && pontoDisparo != null)
        {
            Instantiate(raioPrefab, pontoDisparo.position, Quaternion.identity);
        }
    }

    
    // Desenha o groundCheck no editor (opcional)
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }


}
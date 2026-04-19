using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode shootKey = KeyCode.Space;
    public KeyCode jumpKey = KeyCode.L; // Tecla L para pular

    public float velocidade = 8f;
    public float forcaPulo = 10f; // Força do pulo
    
    public GameObject raioPrefab;
    public Transform pontoDisparo;
    
    public Transform leftWall;
    public Transform rightWall;
    
    private Rigidbody2D rb;
    private float movimentoInput = 0f;
    private bool facingRight = true;
    private bool estaNoChao = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        // CONFIGURAÇÕES CRÍTICAS
        rb.gravityScale = 2f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.mass = 1f;
        rb.linearDamping = 0f;
        
        // Ajusta o collider do player
        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        if (boxCol != null)
        {
            boxCol.edgeRadius = 0.1f;
        }
    }
    
    void Update()
    {
        // Input
        movimentoInput = 0f;
        if (Input.GetKey(moveLeft)) movimentoInput = -1f;
        if (Input.GetKey(moveRight)) movimentoInput = 1f;
        
        // PULO - Tecla L
        if (Input.GetKeyDown(jumpKey) && estaNoChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }
        
        // Verificar se está no chão (usando Raycast simples)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);
        estaNoChao = hit.collider != null;
        
        // Limites
        if (leftWall != null && rightWall != null)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, leftWall.position.x, rightWall.position.x);
            transform.position = pos;
        }
        
        // Atirar
        if (Input.GetKeyDown(shootKey))
        {
            Atirar();
        }
    }
    
    void FixedUpdate()
    {
        // MOVIMENTO QUE FUNCIONA EM RAMPAS
        Vector2 targetVelocity = new Vector2(movimentoInput * velocidade, rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;
    }
    
    void Atirar()
    {
        if (raioPrefab != null && pontoDisparo != null)
        {
            Instantiate(raioPrefab, pontoDisparo.position, Quaternion.identity);
        }
    }
}
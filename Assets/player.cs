using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode shootKey = KeyCode.Space;

    public float velocidade = 8f;
    
    public GameObject raioPrefab;
    public Transform pontoDisparo;
    
    public Transform leftWall;
    public Transform rightWall;
    
    private Rigidbody2D rb;
    private float movimentoInput = 0f;
    private bool facingRight = true;
    
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
            boxCol.edgeRadius = 0.1f; // Bordas arredondadas ajudam a subir rampas
        }
    }
    
    void Update()
    {
        // Input
        movimentoInput = 0f;
        if (Input.GetKey(moveLeft)) movimentoInput = -1f;
        if (Input.GetKey(moveRight)) movimentoInput = 1f;
        
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
        // Método 1: Velocity (mais direto)
        Vector2 targetVelocity = new Vector2(movimentoInput * velocidade, rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;
        
        // Método 2: Se não funcionar, descomente a linha abaixo e comente a de cima
        // rb.AddForce(new Vector2(movimentoInput * velocidade * 10f, 0), ForceMode2D.Force);
    }
    
    void Atirar()
    {
        if (raioPrefab != null && pontoDisparo != null)
        {
            Instantiate(raioPrefab, pontoDisparo.position, Quaternion.identity);
        }
    }
}
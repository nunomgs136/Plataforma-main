using UnityEngine;

public class PralaxVelocity : MonoBehaviour
{
    private float length;
    private float startPos;
    public float parallaxIntensity = 0.5f; // Intensidade do efeito
    public Transform player;
    private Rigidbody2D playerRb;
    
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        
        playerRb = player.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (player == null || playerRb == null) return;
        
        // Move baseado na velocidade do player (direção oposta)
        float movement = playerRb.linearVelocity.x * parallaxIntensity * Time.deltaTime;
        transform.position += Vector3.left * movement;
        
        // Loop infinito
        if (transform.position.x < startPos - length)
            transform.position = new Vector3(startPos + length, transform.position.y, transform.position.z);
        else if (transform.position.x > startPos + length)
            transform.position = new Vector3(startPos - length, transform.position.y, transform.position.z);
    }
}
using UnityEngine;

public class Pralax : MonoBehaviour
{
    private float length;
    private float startPos;
    public float parallaxEffect; // Valores menores = mais distante (ex: 0.1f, 0.3f, 0.5f)
    public Transform player; // Arraste o player aqui no Inspector
    
    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
        // Se não atribuiu o player, tenta encontrar
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        if (player == null) return;
        
        // Calcula o deslocamento baseado na posição do player
        float distance = player.position.x * parallaxEffect;
        
        // Aplica o movimento inverso (multiplica por -1)
        float newX = startPos + (distance * -1);
        
        // Mantém dentro dos limites para looping
        if (Mathf.Abs(newX - startPos) > length)
        {
            startPos += length * Mathf.Sign(newX - startPos);
            newX = startPos + (distance * -1);
        }
        
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
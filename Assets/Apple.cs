using UnityEngine;

public class Apple : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Encontra o GameManager e chama o método para coletar a maçã
            FindObjectOfType<GameManager>().CollectApple();
            
            // Destroi a maçã da tela
            Destroy(gameObject);
        }
    }
}
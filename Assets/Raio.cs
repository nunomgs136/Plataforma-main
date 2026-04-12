using UnityEngine;

public class Raio : MonoBehaviour
{
    public float velocidade = 15f;
    public Vector3 direcao = Vector3.right;

    public bool tiroInimigo = false;

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

void OnTriggerEnter2D(Collider2D col)
{
    //Debug.Log("Raio colidiu com: " + col.gameObject.name + " | Tag: " + col.gameObject.tag);

    if (!tiroInimigo && col.CompareTag("Inimigo"))
    {
        //gameManager.instance.InimigoDestruido();
        Destroy(col.gameObject);
        Destroy(gameObject);
    }

    /*if (!tiroInimigo && col.CompareTag("NaveMae"))
{
    Debug.Log("Acertou NaveMae");

    NaveMae nave = col.GetComponent<NaveMae>();

    if (nave != null)
    {
        nave.AcertouNaveMae();
    }

    Destroy(gameObject);
} */

    /*if (tiroInimigo && col.CompareTag("Player"))
    {
        GameManager.instance.PlayerAtingido();
        Destroy(gameObject);
    } */

    // Destrói o raio ao sair por qualquer parede
    if (!tiroInimigo && col.CompareTag("Wall"))
        Destroy(gameObject);
}

void OnCollisionEnter2D(Collision2D col)
{
    Debug.Log("COLIDIU COM: " + col.gameObject.name);
}
}
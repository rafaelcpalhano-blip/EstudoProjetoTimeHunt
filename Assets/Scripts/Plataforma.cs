using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField] private GameObject[] checkpoints;
    private int checkpointAtual;
    [SerializeField] private float velocidade = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(checkpoints[checkpointAtual].transform.position, transform.position) < 0.1f)
        {
            checkpointAtual++;
                if (checkpointAtual >= checkpoints.Length) { 
            checkpointAtual = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, checkpoints[checkpointAtual].transform.position, Time.deltaTime * velocidade);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}

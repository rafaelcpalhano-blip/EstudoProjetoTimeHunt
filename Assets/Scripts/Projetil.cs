using UnityEngine;

public class Projetil : MonoBehaviour
{
    private int dano;
    private int velocidade;

    [SerializeField] private GameObject explosao;

    private bool ignorarInimigos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(DestruirObjeto), 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * velocidade);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.tag != "Inimigo" && !ignorarInimigos))
        {
            collision.gameObject.GetComponent<Vida>()?.ReduzirVida(dano);
        }

        DestruirObjeto();


    }

    public void IniciarLancamento(
        Transform alvo,
        int velocidade,
        int dano,
        bool ignorarInimigos)
    {
        if(alvo != null)
        {
            transform.right = alvo.position - transform.position;
        }


        this.velocidade = velocidade;
        this.dano = dano;
        this.ignorarInimigos = ignorarInimigos;
    }

    public void DestruirObjeto()
    {
        Instantiate(explosao, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
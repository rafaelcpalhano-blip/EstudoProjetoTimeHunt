using UnityEngine;

public class movimento : MonoBehaviour
{
    private Rigidbody2D rb;
    private float entradaHorizontal;
    [SerializeField] private float velocidade = 5f;
    
    private bool estaNoChao;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask cenarioLayer;

    private bool saltoExtra;
    private DirecaoPersonagem direcaoAtual;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direcaoAtual = DirecaoPersonagem.DIREITA;
    }

    // Update is called once per frame
    void Update()
    {
        entradaHorizontal = Input.GetAxis("Horizontal");
        estaNoChao = Physics2D.OverlapCircle(peDoPersonagem.position, 0.3f, cenarioLayer);

        if (estaNoChao)
        {
            saltoExtra = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (estaNoChao)
            {
                ExecutarSalto();

            } else if(saltoExtra) {
                saltoExtra = false;
                ExecutarSalto();
            }
        }

        if(entradaHorizontal > 0)
        {
            GirarPersonagem(DirecaoPersonagem.DIREITA);
        }else if (entradaHorizontal < 0)
        {
            GirarPersonagem(DirecaoPersonagem.ESQUERDA);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(entradaHorizontal * velocidade, rb.linearVelocity.y);
    }

    private void ExecutarSalto()
    {
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    rb.AddForce(Vector2.up * 300f);
    }

    private void GirarPersonagem(DirecaoPersonagem direcao)
    {
            if(direcao == direcaoAtual)
        {
            return;
        }
        direcaoAtual = direcao;

        if(direcaoAtual == DirecaoPersonagem.DIREITA)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

}

enum DirecaoPersonagem { ESQUERDA, DIREITA }
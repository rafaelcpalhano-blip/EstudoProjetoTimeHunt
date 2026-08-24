using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private Rigidbody2D rb;
    private float entradaHorizontal;
    [SerializeField] private float velocidade = 5f;
    
    private bool estaNoChao;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask cenarioLayer;

    private bool saltoExtra;
    private DirecaoPersonagem direcaoAtual;

    private Animator animator;

    private bool dashLiberadoParaUso = true;
    private bool executandoDash;

    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] private JogadorUI jogadorUI;

    private float velocidadeOriginal;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direcaoAtual = DirecaoPersonagem.DIREITA;

        animator = GetComponent<Animator>();
        trailRenderer.emitting = false;
        velocidadeOriginal = velocidade;
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

        if(Input.GetKeyDown(KeyCode.C) && dashLiberadoParaUso)
        {
            StartCoroutine(RealizarDash());
        }

        if(entradaHorizontal > 0)
        {
            GirarPersonagem(DirecaoPersonagem.DIREITA);
        }else if (entradaHorizontal < 0)
        {
            GirarPersonagem(DirecaoPersonagem.ESQUERDA);
        }

        animator.SetBool("Correr", entradaHorizontal != 0);
        animator.SetBool("EstaNoChao", estaNoChao);

    }
    private void FixedUpdate()
    {
        if(!executandoDash)
        {
            rb.linearVelocity = new Vector2(entradaHorizontal * velocidade, rb.linearVelocity.y);
        }
    }

    private void ExecutarSalto()
    {
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    rb.AddForce(Vector2.up * 300f);

        animator.SetTrigger("Saltar");
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

    private IEnumerator RealizarDash()
    {
        trailRenderer.emitting = true;

        dashLiberadoParaUso = false;
        executandoDash = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        if(direcaoAtual == DirecaoPersonagem.DIREITA)
        {
            rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
        } else
        {
            rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.3f);

        trailRenderer.emitting = false;

        executandoDash = false;
        rb.gravityScale = 1;
        rb.linearVelocity = Vector2.zero;


        float contador = 0;
        while (contador < 3f)
        {
            contador += Time.deltaTime;
            jogadorUI.AtualizarProcessoDash(contador / 3f);
            yield return null;
        }

        dashLiberadoParaUso = true;
    }

    public void AumentarVelocidade()
    {
        velocidade = velocidadeOriginal * 1.5f;
    }

    public void ReduzirVelocidade()
    {
        velocidade = velocidadeOriginal;
    }
}

enum DirecaoPersonagem { ESQUERDA, DIREITA }
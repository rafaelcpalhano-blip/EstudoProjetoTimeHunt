using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private Rigidbody2D rb;
    private float entradaHorizontal;
    [SerializeField] private float velocidade = 5f;
    
    private bool estaNoChao;
    private bool estaNoCenario;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask cenarioLayer;
    private LayerMask inimigoLayer;

    [SerializeField] private int quantidadeMaximaDePulos = 2;
    [SerializeField] private float forcaDeAderenciaAoChao = 2f;
    [SerializeField] private float multiplicadorDeQueda = 1.5f;
    private int pulosRestantes;
    private bool estavaNoChao;
    private float tempoSemAderencia;
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
        pulosRestantes = quantidadeMaximaDePulos;
        inimigoLayer = LayerMask.GetMask("Inimigo");
    }

    // Update is called once per frame
    void Update()
    {
        if (ControladorPartida.Instance != null && ControladorPartida.Instance.JogoInterrompido)
        {
            return;
        }

        entradaHorizontal = Input.GetAxis("Horizontal");
        estaNoCenario = Physics2D.OverlapCircle(
            peDoPersonagem.position,
            0.3f,
            cenarioLayer
        );

        bool estaSobreInimigo = VerificarSeEstaSobreInimigo();

        estaNoChao = estaNoCenario || estaSobreInimigo;

        if (estaNoChao && !estavaNoChao)
        {
            pulosRestantes = quantidadeMaximaDePulos;
        }

        estavaNoChao = estaNoChao;
        tempoSemAderencia -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && pulosRestantes > 0)
        {
            pulosRestantes--;
            ExecutarSalto();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashLiberadoParaUso)
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

    private bool VerificarSeEstaSobreInimigo()
    {
        RaycastHit2D[] contatos = Physics2D.RaycastAll(
            peDoPersonagem.position + Vector3.up * 0.1f,
            Vector2.down,
            0.3f,
            inimigoLayer
        );

        foreach (RaycastHit2D contato in contatos)
        {
            if (!contato.collider.isTrigger && contato.normal.y > 0.5f)
            {
                return true;
            }
        }

        return false;
    }

    private void FixedUpdate()
    {
        if(!executandoDash)
        {
            if (estaNoCenario && tempoSemAderencia <= 0f)
            {
                RaycastHit2D contatoComChao = Physics2D.Raycast(
                    peDoPersonagem.position + Vector3.up * 0.1f,
                    Vector2.down,
                    0.5f,
                    cenarioLayer
                );

                if (contatoComChao.collider != null)
                {
                    Vector2 normalDoChao = contatoComChao.normal;
                    Vector2 direcaoDaRampa = new Vector2(normalDoChao.y, -normalDoChao.x);
                    Vector2 movimentoNaRampa = direcaoDaRampa * (entradaHorizontal * velocidade);
                    Vector2 aderencia = -normalDoChao * forcaDeAderenciaAoChao;

                    rb.linearVelocity = movimentoNaRampa + aderencia;
                }
                else
                {
                    rb.linearVelocity = new Vector2(entradaHorizontal * velocidade, rb.linearVelocity.y);
                }
            }
            else
            {
                rb.linearVelocity = new Vector2(entradaHorizontal * velocidade, rb.linearVelocity.y);
            }

            if (!estaNoChao && rb.linearVelocity.y < 0f)
            {
                rb.AddForce(Physics2D.gravity * (multiplicadorDeQueda - 1f) * rb.mass);
            }
        }
    }

    private void ExecutarSalto()
    {
        tempoSemAderencia = 0.15f;
        estaNoChao = false;
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

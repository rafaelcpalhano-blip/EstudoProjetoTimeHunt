using System.Collections;
using UnityEngine;

public class Ataque : MonoBehaviour
{

    private bool espadaLiberadaParaUso = true;
    private Animator animator;

    [SerializeField] private ControladorHitbox controladorHitbox;
    [SerializeField] private int danoEspada = 30;

    [SerializeField] private JogadorUI jogadorUI;

    private bool bolaDeFogoLiberadoParaUso = true;
    [SerializeField] private Projetil bolaDeFogoPrefab;
    [SerializeField] private Transform pontoDeLancamento;

    [SerializeField]private int danoBolaDeFogo = 50;
    [SerializeField] private int velocidadeBolaDeFogo = 5;

    private int danoEspadaOriginal;
    private int danoBolaDeFogoOriginal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        danoEspadaOriginal = danoEspada;
        danoBolaDeFogoOriginal = danoBolaDeFogo;
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Mouse0) && espadaLiberadaParaUso)
        {
            StartCoroutine(RealizarAtaqueComEspada());
        }

         if(Input.GetKeyDown(KeyCode.Mouse1) && bolaDeFogoLiberadoParaUso)
        {
            StartCoroutine(RealizarAtaqueComBolaDeFogo());
        }
        
    }

    private IEnumerator RealizarAtaqueComEspada()
    {
        espadaLiberadaParaUso = false;
        animator.SetTrigger("AtaqueComEspada");
        controladorHitbox.AplicarDano(danoEspada);

        float contador = 0;
        while(contador < 0.6f)
        {
            contador += Time.deltaTime;
            jogadorUI.AtualizarProcessoEspada(contador / 0.6f);
            yield return null;
        }

        espadaLiberadaParaUso = true;

    }

    private IEnumerator RealizarAtaqueComBolaDeFogo()
    {
        bolaDeFogoLiberadoParaUso = false;
        animator.SetTrigger("AtaqueComBolaDeFogo");
        yield return new WaitForSeconds(0.3f);

        Projetil projetil = Instantiate(bolaDeFogoPrefab, pontoDeLancamento.position, pontoDeLancamento.rotation);
        projetil.IniciarLancamento(null, velocidadeBolaDeFogo, danoBolaDeFogo, false);

        float contador = 0;
        while (contador < 3f)
        {
            contador += Time.deltaTime;
            jogadorUI.AtualizarProcessoBolaDeFogo(contador / 3f);
                yield return null;
        }

        bolaDeFogoLiberadoParaUso = true;
    }

    public void AumentarDano()
    {
        danoEspada = danoEspadaOriginal * 2;
        danoBolaDeFogo = danoBolaDeFogoOriginal * 2;
    }

    public void ReduzirDano()
    {
        danoEspada = danoEspadaOriginal;
        danoBolaDeFogo = danoBolaDeFogoOriginal;
    }
}

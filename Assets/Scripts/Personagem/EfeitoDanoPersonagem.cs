using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EfeitoDanoPersonagem : MonoBehaviour
{
    [SerializeField] private int quantidadeDePiscadas = 3;
    [SerializeField] private float duracaoDoEfeito = 1.5f;

    private SpriteRenderer spriteRenderer;
    private Coroutine efeitoAtual;
    private Color corOriginal;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
    }

    public void PiscarAoReceberDano(int danoRecebido, int vidaAtual)
    {
        if (efeitoAtual != null)
        {
            StopCoroutine(efeitoAtual);
            spriteRenderer.color = corOriginal;
        }

        efeitoAtual = StartCoroutine(Piscar());
    }

    private IEnumerator Piscar()
    {
        float intervalo = duracaoDoEfeito / (quantidadeDePiscadas * 2f);

        for (int i = 0; i < quantidadeDePiscadas; i++)
        {
            AlterarVisibilidade(false);
            yield return new WaitForSeconds(intervalo);

            AlterarVisibilidade(true);
            yield return new WaitForSeconds(intervalo);
        }

        spriteRenderer.color = corOriginal;
        efeitoAtual = null;
    }

    private void AlterarVisibilidade(bool visivel)
    {
        Color corAtual = corOriginal;
        corAtual.a = visivel ? corOriginal.a : 0f;
        spriteRenderer.color = corAtual;
    }

    private void OnDisable()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = corOriginal;
        }
    }
}

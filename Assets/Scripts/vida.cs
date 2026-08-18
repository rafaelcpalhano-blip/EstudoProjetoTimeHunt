using UnityEngine;
using UnityEngine.Events;

public class vida : MonoBehaviour
{
    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaAtual;

    [SerializeField] private UnityEvent<int, int> OnAtualizarVidaMaxima;
    [SerializeField] private UnityEvent<int, int> OnReduzirVida;
    [SerializeField] private UnityEvent<int, int> OnAumentarVida;
    [SerializeField] private UnityEvent OnMorrer;

    private void Start()
    {
        AtualizarVidaMaxima(vidaMaxima, vidaAtual);
    }

    public void AtualizarVidaMaxima(int vidaMaxima, int vidaAtual)
    {
        this.vidaMaxima = vidaMaxima;
        this.vidaAtual = vidaAtual;

        OnAtualizarVidaMaxima.Invoke(vidaMaxima, vidaAtual);
    }

    public void ReduzirVida(int danoRecebido)
    {
        vidaAtual -= danoRecebido;
        OnReduzirVida.Invoke(danoRecebido, vidaAtual);

        if(vidaAtual <= 0)
        {
            OnMorrer.Invoke();
        }
    }

    public void AumentarVida(int aumentoRecebido)
    {
        vidaAtual += aumentoRecebido;

        if(vidaAtual > vidaMaxima)
        {
            vidaAtual = vidaMaxima;
        }
        
        OnAumentarVida.Invoke(aumentoRecebido, vidaAtual);
    }
}

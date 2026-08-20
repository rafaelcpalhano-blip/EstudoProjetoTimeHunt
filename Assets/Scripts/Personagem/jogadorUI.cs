using UnityEngine;
using UnityEngine.UI;

public class JogadorUI : MonoBehaviour
{
    [SerializeField] private Image espadaProgressoImage;
    [SerializeField] private Image bolaDeFogoProgressoImage;
    [SerializeField] private Image dashProgressoImage;

    [SerializeField] private Slider barraDeVidaSlider;

    public void AtualizarProcessoEspada(float progresso)
    {
        espadaProgressoImage.fillAmount = progresso;
    }


    public void AtualizarProcessoBolaDeFogo(float progresso)
    {
        bolaDeFogoProgressoImage.fillAmount = progresso;
    }



    public void AtualizarProcessoDash(float progresso)
    {
        dashProgressoImage.fillAmount = progresso;
    }

    public void AtualizarVidaMaxima(int vidaMaxima, int vidaAtual)
    {
        barraDeVidaSlider.maxValue = vidaMaxima;
        barraDeVidaSlider.value = vidaAtual;
    }

    public void AtualizarVidaAtual(int modificador, int vidaAtual)
    {
        barraDeVidaSlider.value = vidaAtual;
    }
}

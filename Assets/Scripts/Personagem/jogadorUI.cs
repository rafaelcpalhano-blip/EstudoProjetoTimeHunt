using UnityEngine;
using UnityEngine.UI;

public class JogadorUI : MonoBehaviour
{
    [SerializeField] private Image espadaProgressoImage;
    [SerializeField] private Image bolaDeFogoProgressoImage;
    [SerializeField] private Image dashProgressoImage;

    [SerializeField] private Image powerupInvencivelProgresso;
    [SerializeField] private Image powerupVelocidadeProgresso;
    [SerializeField] private Image powerupDano2XProgresso;

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

    public void AtualizarProgressoPowerup(TipoPowerup powerup, float progresso)
    {

        progresso = 1 - progresso;
        switch (powerup)
        {
            case TipoPowerup.INVENSIVEL:
                powerupInvencivelProgresso.fillAmount = progresso;
                break;
            case TipoPowerup.VELOCIDADE:
                powerupVelocidadeProgresso.fillAmount = progresso;
                break;
            case TipoPowerup.DANO2X:
                powerupDano2XProgresso.fillAmount = progresso;
                break;
        }
    }

    public void AlterarVisibilidadePowerup(TipoPowerup powerup, bool ativado)
    {
        switch (powerup)
        {
            case TipoPowerup.INVENSIVEL:
                powerupInvencivelProgresso.transform.parent.gameObject.SetActive(ativado);
                break;
            case TipoPowerup.VELOCIDADE:
                powerupVelocidadeProgresso.transform.parent.gameObject.SetActive(ativado);
                break;
            case TipoPowerup.DANO2X:
                powerupDano2XProgresso.transform.parent.gameObject.SetActive(ativado);
                break;
        }
    }
}
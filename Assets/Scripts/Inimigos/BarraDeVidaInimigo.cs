using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaInimigo : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Start()
    {
        slider.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        slider.transform.rotation = Quaternion.identity;
    }

    public void AtualizarVidaMaxima(int vidaMaxima, int vidaAtual)
    {
        slider.maxValue = vidaMaxima;
        slider.value = vidaAtual;
    }

    public void AtualizarVidaAtual(int modificador, int vidaAtual)
    {
        slider.value = vidaAtual;
        slider.gameObject.SetActive(vidaAtual > 0);
    }
}

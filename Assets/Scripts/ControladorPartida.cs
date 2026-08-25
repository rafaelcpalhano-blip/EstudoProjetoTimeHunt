using System.Collections;
using TMPro;
using UnityEngine;

public class ControladorPartida : MonoBehaviour
{
    public static ControladorPartida Instance { get; private set; }

    private int tempoRestante = 30;
    private int tempoTotalDePartida;

    private int monstrosDerrotados;
    private int danoSofrido;
    private int chavesColetadas;

    [SerializeField] private TMP_Text tempoRestanteText;

    [SerializeField] private GameObject gameoverPanel;

    [SerializeField] private TMP_Text chavesColetadasText;


    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }else
        {
            Instance = this;   
        }
    }


    void Start()
    {
        StartCoroutine(ContadorDeTempo());
    }


    private IEnumerator ContadorDeTempo()
    {
        while(tempoRestante > 0)
        {
            yield return new WaitForSeconds(1);
            tempoRestante--;
            tempoTotalDePartida++;

            tempoRestanteText.text = tempoRestante + "s";
        }

        FinalizarPartida(false);
    }

    public void FinalizarPartida(bool vitoria)
    {
        Time.timeScale = 0;

        gameoverPanel.SetActive(true);
    }

    public void NovoMonstroDerrotado(int tempoExtra)
    {
        monstrosDerrotados++;
        tempoRestante += tempoExtra;
    }


    public void AdicionarDanoSofrido(int danoRecebido, int vidaAtual)
    {
        danoSofrido += danoRecebido;
    }

    public void NovaChaveColetada()
    {
        chavesColetadas++;

        chavesColetadasText.text = chavesColetadas + "/3";
                
        if (chavesColetadas >= 3)
        {
            FinalizarPartida(true);
        }
    }
}
 
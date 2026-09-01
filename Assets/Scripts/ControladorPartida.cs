using System.Collections;
using TMPro;
using Unity.VectorGraphics;
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


    [SerializeField] private TMP_Text tempoJogadoGameoverText;
    [SerializeField] private TMP_Text monstrosDerrotadosGameoverText;
    [SerializeField] private TMP_Text danoSofridoGameoverText;
    [SerializeField] private TMP_Text chavesColetadasGameoverText;
    [SerializeField] private TMP_Text scoreGameoverText;

    [SerializeField] private CloudServices cloudServices;
    [SerializeField] private string nomeDaTabelaDeClassificacao;

    [SerializeField] private AudioSource gameoverAudioSource;

    [SerializeField] private bool timerAtivo = true;

    [SerializeField] private AudioSource dezSegundosAudioSource;

    [Header("Pause")]
    [SerializeField, Range(0f, 1f)] private float volumeDaMusicaNoPause = 0.35f;
    [SerializeField] private GameObject pausePanel;

    private AudioSource trilhaSonoraAudioSource;
    private float volumeOriginalDaMusica;
    private bool partidaPausada;
    private bool partidaFinalizada;

    public bool JogoInterrompido => partidaPausada || partidaFinalizada;


    void Awake()
    {
        Time.timeScale = 1f;

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
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        GameObject trilhaSonora = GameObject.Find("TrilhaSonora");

        if (trilhaSonora != null)
        {
            trilhaSonoraAudioSource = trilhaSonora.GetComponent<AudioSource>();

            if (trilhaSonoraAudioSource != null)
            {
                volumeOriginalDaMusica = trilhaSonoraAudioSource.volume;
            }
        }

        tempoRestanteText.gameObject.SetActive(timerAtivo);

        if (timerAtivo)
        {
            StartCoroutine(ContadorDeTempo());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !partidaFinalizada)
        {
            AlternarPause();
        }
    }

    private void AlternarPause()
    {
        partidaPausada = !partidaPausada;
        Time.timeScale = partidaPausada ? 0f : 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(partidaPausada);
        }

        if (trilhaSonoraAudioSource != null)
        {
            trilhaSonoraAudioSource.volume = partidaPausada
                ? volumeOriginalDaMusica * volumeDaMusicaNoPause
                : volumeOriginalDaMusica;
        }
    }


    private IEnumerator ContadorDeTempo()
    {
        while(tempoRestante > 0)
        {
            yield return new WaitForSeconds(1);
            tempoRestante--;
            tempoTotalDePartida++;

            tempoRestanteText.text = tempoRestante + "s";

            if(tempoRestante == 10)
            {
                dezSegundosAudioSource.Play();
            }
        }

        FinalizarPartida(false);
    }

    public void FinalizarPartida(bool vitoria)
    {
        if (partidaFinalizada)
        {
            return;
        }

        partidaFinalizada = true;
        partidaPausada = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (trilhaSonoraAudioSource != null)
        {
            trilhaSonoraAudioSource.volume = volumeOriginalDaMusica;
        }

        gameoverAudioSource.Play();
        Time.timeScale = 0;

        gameoverPanel.SetActive(true);

        tempoJogadoGameoverText.text = tempoTotalDePartida + "s";
        monstrosDerrotadosGameoverText.text = monstrosDerrotados.ToString();
        danoSofridoGameoverText.text = danoSofrido.ToString();
        chavesColetadasGameoverText.text = chavesColetadas + "/3";

        if(vitoria)
        {
            scoreGameoverText.text = "Score: " + Mathf.Max(0, CalcularScore());
            cloudServices.RegistrarNovaPontuacao(nomeDaTabelaDeClassificacao, Mathf.Max(0, CalcularScore()));

        }
        else
        {
            scoreGameoverText.text = "SCORE = 0000";
        }

    }

    private int CalcularScore()
    {
        return (2000 - tempoTotalDePartida) + monstrosDerrotados * 5 - danoSofrido * 2;
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

    private void OnDisable()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (trilhaSonoraAudioSource != null)
        {
            trilhaSonoraAudioSource.volume = volumeOriginalDaMusica;
        }

        if (partidaPausada)
        {
            Time.timeScale = 1f;
        }
    }
}

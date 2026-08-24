using System.Collections;
using UnityEngine;

public class ControladorPowerup : MonoBehaviour
{
    private Coroutine invencivelCoroutine;
    private Coroutine velocidadeCoroutine;
    private Coroutine dano2XCoroutine;

    [SerializeField]private JogadorUI jogadorUI;

    void Start()
    {
        
    }
    
    public void EquiparPowerup(TipoPowerup tipo)
    {
        switch (tipo)
        {
            case TipoPowerup.INVENSIVEL:
                if (invencivelCoroutine != null)
                {
                    StopCoroutine(invencivelCoroutine);
                }
                    invencivelCoroutine = StartCoroutine(AtivarInvencivel());
                break;
            case TipoPowerup.CURA:
                GetComponent<Vida>().AumentarVida(40);
                break;
            case TipoPowerup.VELOCIDADE:
                if(velocidadeCoroutine != null)
                {
                    StopCoroutine(velocidadeCoroutine);
                }
                velocidadeCoroutine = StartCoroutine(AtivarVelocidade());
                break;
            case TipoPowerup.DANO2X:
                if (dano2XCoroutine != null)
                {
                    StopCoroutine(dano2XCoroutine);
                }
                dano2XCoroutine = StartCoroutine(AtivarDano2X());
               
                break;
        }
    }

    private IEnumerator AtivarInvencivel()
    {
        gameObject.layer = LayerMask.NameToLayer("Intangivel");
        jogadorUI.AlterarVisibilidadePowerup(TipoPowerup.INVENSIVEL, true);
        float contador = 0f;
        while(contador < 5f)
        {
            contador += Time.deltaTime;
            jogadorUI.AtualizarProgressoPowerup(TipoPowerup.INVENSIVEL, contador / 5f);
            yield return null;
        }
        jogadorUI.AlterarVisibilidadePowerup(TipoPowerup.INVENSIVEL, false);

        gameObject.layer = LayerMask.NameToLayer("Personagem");
    }

    private IEnumerator AtivarVelocidade()
    {
        jogadorUI.AlterarVisibilidadePowerup(TipoPowerup.VELOCIDADE, true);
        GetComponent<Movimento>().AumentarVelocidade();

        float contador = 0f;
        while (contador < 10f)
        {
            contador += Time.deltaTime;
            jogadorUI.AtualizarProgressoPowerup(TipoPowerup.VELOCIDADE, contador / 10f);
            yield return null;
        }

        GetComponent<Movimento>().ReduzirVelocidade();
        jogadorUI.AlterarVisibilidadePowerup(TipoPowerup.VELOCIDADE, false);
    }

    private IEnumerator AtivarDano2X()
    {
        jogadorUI.AlterarVisibilidadePowerup(TipoPowerup.DANO2X, true);
        GetComponent<Ataque>().AumentarDano();

        float contador = 0f;
        while (contador < 10f)
        {
            contador += Time.deltaTime;
            jogadorUI.AtualizarProgressoPowerup(TipoPowerup.DANO2X, contador / 10f);
            yield return null;
        }

        GetComponent<Ataque>().ReduzirDano();
        jogadorUI.AlterarVisibilidadePowerup(TipoPowerup.DANO2X, false);
    }
}
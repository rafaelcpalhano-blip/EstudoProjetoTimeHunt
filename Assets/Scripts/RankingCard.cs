using TMPro;
using UnityEngine;

public class RankingCard : MonoBehaviour
{
    [SerializeField] private TMP_Text posicaoText;
    [SerializeField] private TMP_Text nomeText;
    [SerializeField] private TMP_Text pontuacaoText;

    public void IniciarCard(JogadorRanking jogadorRanking)
    {
        posicaoText.text = jogadorRanking.posicao + "º";
        nomeText.text = jogadorRanking.username;
        pontuacaoText.text = jogadorRanking.pontuacao.ToString();
    }
}

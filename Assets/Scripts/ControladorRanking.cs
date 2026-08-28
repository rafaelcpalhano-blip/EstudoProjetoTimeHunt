using System.Collections.Generic;
using UnityEngine;

public class ControladorRanking : MonoBehaviour
{
    [SerializeField] private RankingCard cardPrefab;
    [SerializeField] private CloudServices cloudServices;
    [SerializeField] private Transform scrollContent;


    public async void CarregarRanking(string nomeDaTabela)
    {
        foreach(Transform child in scrollContent)
        {
            Destroy(child.gameObject);
        }

        List<JogadorRanking> cards = await cloudServices.GetRanking(nomeDaTabela);

        foreach(JogadorRanking card in cards) 
        {
            RankingCard rc = Instantiate(cardPrefab, scrollContent);
            rc.IniciarCard(card);
        }
    }
}

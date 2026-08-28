using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class CloudServices : MonoBehaviour
{
    public async Task RealizarLogin()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            if(AuthenticationService.Instance.PlayerName == "" || AuthenticationService.Instance.PlayerName == null)
            {
                await AtualizarUsername("Player");
            }

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public async Task AtualizarUsername(string username)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(username);
    }

    public string GetUsername()
    {
        return AuthenticationService.Instance.PlayerName;
    }

    public async Task RegistrarNovaPontuacao(string nomeDaTabela, int pontuacao)
    {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(nomeDaTabela, pontuacao);
    }

    public async Task<List<JogadorRanking>> GetRanking(string nomeDaTabela)
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(nomeDaTabela);

        List<LeaderboardEntry> list = scoresResponse.Results;
        List<JogadorRanking> cards = new List<JogadorRanking>();

        foreach(LeaderboardEntry i in list)
        {
            JogadorRanking jogadorRanking = new JogadorRanking();
            jogadorRanking.posicao = i.Rank + 1;
            jogadorRanking.username = i.PlayerName;
            jogadorRanking.pontuacao = (int)i.Score;

            cards.Add(jogadorRanking);
        }
        return cards;
    }
}

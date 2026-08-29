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
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");
            }

            if (string.IsNullOrEmpty(AuthenticationService.Instance.PlayerName))
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
            throw;
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            throw;
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
        await RealizarLogin();
        await LeaderboardsService.Instance.AddPlayerScoreAsync(nomeDaTabela, pontuacao);
    }

    public async Task<List<JogadorRanking>> GetRanking(string nomeDaTabela)
    {
        await RealizarLogin();
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

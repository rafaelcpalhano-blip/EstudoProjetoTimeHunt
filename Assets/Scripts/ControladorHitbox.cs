using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ControladorHitbox : MonoBehaviour
{
   [SerializeField] private List<GameObject> alvosDentroDaArea;
   [SerializeField] private string alvoTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(alvoTag))
        {
            alvosDentroDaArea.Add(collision.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag(alvoTag))
        {
            alvosDentroDaArea.Remove(collision.gameObject);
        }
    }

    public bool ExisteAlvosDisponiveis()
    {
        return alvosDentroDaArea.Count > 0;
    }

    public void AplicarDano(int dano)
    {
        for(int i = 0; i < alvosDentroDaArea.Count; i++)
        {
            alvosDentroDaArea[i].GetComponent<vida>().ReduzirVida(dano);
        }
    }
}

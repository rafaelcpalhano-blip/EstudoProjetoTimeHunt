using UnityEngine;

public class ZonaMortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Vida vidaDoPersonagem = collision.GetComponent<Vida>();

        if (vidaDoPersonagem != null)
        {
            vidaDoPersonagem.MorrerInstantaneamente();
        }
    }
}
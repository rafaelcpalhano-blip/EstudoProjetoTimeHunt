using UnityEngine;

public class ArmadilhaEspinhos : MonoBehaviour
{
    [SerializeField] private int dano;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Vida>()?.ReduzirVida(dano);
        }
    }
}

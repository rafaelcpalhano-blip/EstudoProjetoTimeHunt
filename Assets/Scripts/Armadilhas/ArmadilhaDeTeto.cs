using UnityEngine;

public class ArmadilhaDeTeto : MonoBehaviour
{
    [SerializeField] private int dano;
        private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        InvokeRepeating(nameof(AtivarArmadilha), Random.Range(1, 5), Random.Range(2, 5));
    }

    private void AtivarArmadilha()
    {
        animator.SetTrigger("Atacar");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Vida>()?.ReduzirVida(dano);
        }
    }
}

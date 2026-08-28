using UnityEngine;

public class ArmadilhaLancarProjetil : MonoBehaviour
{
    [SerializeField] private Transform[] pontosDeLancamento;
    [SerializeField] private Projetil objetoInstanciavel;
    private Animator animator;

    [SerializeField] private int velocidade;
    [SerializeField] private int dano;

    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating(nameof(AtivarArmadilha), Random.Range(3, 10), Random.Range(3, 10));
    }

    private void AtivarArmadilha()
    {
        animator.SetTrigger("Atacar");
    }

    public void InstanciarProjetil()
    {
        foreach (Transform t in pontosDeLancamento)
        {
            Projetil projetil = Instantiate(objetoInstanciavel, t.position, t.rotation);
            projetil.IniciarLancamento(null, velocidade, dano, true);
        }
    }
}

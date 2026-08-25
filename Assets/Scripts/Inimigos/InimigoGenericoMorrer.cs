using System;
using UnityEngine;

public class InimigoGenericoMorrer : InimigoEstado
{
    private Animator animator;

    [SerializeField] private GameObject efeitoDeParticulasMorte;

    [SerializeField]private int tempoExtra;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        ControladorPartida.Instance.NovoMonstroDerrotado(tempoExtra);

        Instantiate(efeitoDeParticulasMorte, transform.position, transform.rotation);

        animator.SetTrigger("Morrer");
        Destroy(gameObject, 3f);
    }

    public override void OnExit()
    {
        
    }

    public override Type OnUpdate()
    {
        return null;
    }



}

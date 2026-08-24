using System;
using UnityEngine;

public class InimigoGenericoMorrer : InimigoEstado
{
    private Animator animator;

    [SerializeField] private GameObject efeitoDeParticulasMorte;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnter()
    {
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

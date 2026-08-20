using System;
using UnityEngine;

public class InimigoGenericoMorrer : InimigoEstado
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnter()
    {
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

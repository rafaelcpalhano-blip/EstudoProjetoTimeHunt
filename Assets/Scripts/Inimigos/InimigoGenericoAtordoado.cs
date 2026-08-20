using System;
using UnityEngine;
using UnityEngine.Rendering;

public class InimigoGenericoAtordoado : InimigoEstado
{
    private Animator animator;
    [SerializeField] private float tempoAtordoado;

    private float contador;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public override void OnEnter()
    {
        animator.SetTrigger("ReceberDano"); 
    }

    public override void OnExit()
    {
        contador = 0; 
    }

    public override Type OnUpdate()
    {
        contador += Time.deltaTime;
            if(contador > tempoAtordoado)
        {
            return typeof(InimigoGenericoMovimento);
        }
        return null;
    }




}
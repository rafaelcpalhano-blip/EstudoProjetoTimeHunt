using System;
using UnityEngine;

public class InimigoGenericoAtaque : InimigoEstado
{
    [SerializeField] private int dano = 30;
    [SerializeField] private ControladorHitbox controladorHitbox;
    [SerializeField] private float tempoDeReacao;

    private Animator animator;
    private Type proximoEstado = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public override void OnEnter()
    {
        Invoke(nameof(IniciarAtaque), tempoDeReacao);
    }

    public override void OnExit()
    {
        proximoEstado = null;
        CancelInvoke();

    }

    public override Type OnUpdate()
    {
        return proximoEstado;
    }


    private void IniciarAtaque()
    {
        animator.SetTrigger("Atacar");
    }


    public void RealizarAtaque()
    {
        controladorHitbox.AplicarDano(dano);
        Invoke(nameof(FinalizarAtaque), tempoDeReacao);
    }

    private void FinalizarAtaque()
    {
        proximoEstado = typeof(InimigoGenericoMovimento);
    }
}

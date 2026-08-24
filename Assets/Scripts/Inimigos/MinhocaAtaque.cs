using System;
using UnityEngine;

public class MinhocaAtaque : InimigoEstado
{

    [SerializeField] private ControladorHitbox controladorHitbox;
    private Animator animator;

    private Transform player;

    [SerializeField] private float intervaloDeAtaque;
    private float contador;

    [SerializeField] private Projetil bolaDeFogoPrefab;
    [SerializeField] private Transform pontoDeLancamento;

    [SerializeField] private int dano;
    [SerializeField] private int velocidade;


    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnEnter()
    {
    
    }

    public override void OnExit()
    {
    
    }

    public override Type OnUpdate()
    {
        GirarInimigo();

        if (controladorHitbox.ExisteAlvosDisponiveis())
        {
            contador += Time.deltaTime;
            if(contador > intervaloDeAtaque)
            {
                contador = 0;
                animator.SetTrigger("Atacar");
            }
        }
        return null;
    }

    public void RealizarAtaque()
    {
        Projetil projetil = Instantiate(bolaDeFogoPrefab, pontoDeLancamento.position, Quaternion.identity);
        projetil.IniciarLancamento(player, velocidade, dano, true);
    }

    private void GirarInimigo()
    {
        if(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}

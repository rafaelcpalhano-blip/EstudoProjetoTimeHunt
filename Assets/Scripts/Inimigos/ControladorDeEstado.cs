using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControladorDeEstado : MonoBehaviour
{
    protected InimigoEstado estadoAtual;
    [SerializeField] protected List<InimigoEstado> estadosDisponiveis;

    private void Update()
    {
        var tipoDoEstado = estadoAtual?.OnUpdate();
        MudarEstado(tipoDoEstado);
    }

    public void MudarEstado(Type estado)
    {
        if (estado != null && estado != estadoAtual?.GetType())
        {
            var novoEstado = estadosDisponiveis.Find(e => e.GetType() == estado);

            if (novoEstado != null)
            {
                estadoAtual?.OnExit();
                estadoAtual = novoEstado;
                estadoAtual.OnEnter();
            }
        }
    }
}

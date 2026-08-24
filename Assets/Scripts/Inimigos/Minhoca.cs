using UnityEngine;

public class Minhoca : ControladorDeEstado
{

    void Start()
    {
        MudarEstado(typeof(MinhocaAtaque));
    }

    public void Morrer()
    {
        MudarEstado(typeof(InimigoGenericoMorrer));
    }
}

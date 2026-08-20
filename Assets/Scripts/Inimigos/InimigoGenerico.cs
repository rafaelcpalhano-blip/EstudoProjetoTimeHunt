using UnityEngine;

public class InimigoGenerico : ControladorDeEstado
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MudarEstado(typeof(InimigoGenericoMovimento));
    }

    public void ReceberDano()
    {
        MudarEstado(typeof(InimigoGenericoAtordoado));
    }

    public void Morrer()
    {
        MudarEstado(typeof (InimigoGenericoMorrer));
    }
}

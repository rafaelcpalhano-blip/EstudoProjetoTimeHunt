using System.Collections;
using UnityEngine;

public class ataque : MonoBehaviour
{

    private bool espadaLiberadaParaUso = true;
    private Animator animator;

    [SerializeField] private ControladorHitbox controladorHitbox;
    [SerializeField] private int danoEspada = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Mouse0) && espadaLiberadaParaUso)
        {
            StartCoroutine(RealizarAtaqueComEspada());
        }
        
    }

    private IEnumerator RealizarAtaqueComEspada()
    {
        espadaLiberadaParaUso = false;
        animator.SetTrigger("AtaqueComEspada");
        controladorHitbox.AplicarDano(danoEspada);

        yield return new WaitForSeconds(0.6f);
        espadaLiberadaParaUso = true;

    }
}

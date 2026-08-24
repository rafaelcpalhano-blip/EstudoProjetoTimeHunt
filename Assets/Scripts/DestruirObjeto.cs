using UnityEngine;
using UnityEngine.Rendering;

public class DestruirObjeto : MonoBehaviour
{
    [SerializeField]private float delay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, delay);
    }

}

using System.Collections.Generic;
using UnityEngine;

public class ControladorChaves : MonoBehaviour
{
    [SerializeField] private GameObject chavePrefab;
    [SerializeField] private List<Transform> pontosDeSpawn;
    [SerializeField] private GameObject efeitoParticulas;

    [SerializeField] private AudioSource chaveColetadaAudioSource;


    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, pontosDeSpawn.Count);
            Instantiate(chavePrefab, pontosDeSpawn[random].position, Quaternion.identity);
            pontosDeSpawn.RemoveAt(random);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Chave")
        {
            chaveColetadaAudioSource.Play();
            Instantiate(efeitoParticulas, collision.transform.position, collision.transform.rotation);
            collision.collider.enabled = false; ;
            ControladorPartida.Instance.NovaChaveColetada();
            Destroy(collision.gameObject);
        }
    }
}

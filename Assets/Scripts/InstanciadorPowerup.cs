using UnityEngine;
using UnityEngine.Rendering;

public class InstanciadorPowerup : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;
    [Range(0f, 1f)][SerializeField] private float probabilidade;

    public void InstanciarPowerup()
    {
        float random = Random.Range(0f, 1f);
        if(random < probabilidade)
        {
            Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, Quaternion.identity);
        }

    }
}

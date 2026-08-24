using UnityEngine;
using UnityEngine.UIElements;

public class Powerup : MonoBehaviour
{
    [SerializeField] private TipoPowerup tipo;

    void Start()
    {
        Invoke(nameof(AtivarColisor), 1f);
    }

    private void AtivarColisor()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControladorPowerup>().EquiparPowerup(tipo);
            Destroy(gameObject);
        }
    }
}

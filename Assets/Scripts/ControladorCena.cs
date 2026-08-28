using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCena : MonoBehaviour
{
public void CarregarNovaCena(int index)
    {
        SceneManager.LoadScene(index); 
    }
}

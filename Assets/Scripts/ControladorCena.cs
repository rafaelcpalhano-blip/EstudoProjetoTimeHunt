using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCena : MonoBehaviour
{
public void CarregarNovaCena(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index); 
    }
}

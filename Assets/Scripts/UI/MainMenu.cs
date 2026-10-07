using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator animNiveis;
    public GameObject Buttons;
    public void Iniciar()
    {
        Buttons.SetActive(false);
    }

    // Update is called once per frame
    public void AbrirNivel()
    {
        Debug.Log("Clicou Animaçao Tocou");
        animNiveis.SetTrigger("Abrir");

    }
}

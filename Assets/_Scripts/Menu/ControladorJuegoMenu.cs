using UnityEngine;

namespace Menu
{
    public class ControladorJuegoMenu : MonoBehaviour
    {
        #region Métodos
        public void IrAEscena(string nombre)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nombre);
        }

        public void Salir()
        {
            Application.Quit();
        }
        #endregion
    }
}
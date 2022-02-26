using UnityEngine;
using UnityEngine.UI;

using UnityUtils;

namespace JugadorCharacterController
{
    class ControladorJuego : MonoBehaviour
    {
        [SerializeField] Text txtCronometro;
        [SerializeField] Text txtTemporizador;

        readonly Tiempo tiempo = new Tiempo();

        private void Start()
        {
            OcultarYBloquearCursor();

            tiempo.alarma = 5;

            MostrarTexto();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                MostrarTexto();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                tiempo.ResetearCronometro();
                tiempo.ResetearTemporizador();

                MostrarTexto();
            }

            if (tiempo.EstaTemporizadorAcabado)
            {
                print("El temporizador ha acabado");
            }
            if (tiempo.SeHaActivadoAlarma)
            {
                print($"Se ha activado la alarma a los {tiempo.alarma} segundos");
            }
        }

        void OcultarYBloquearCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void MostrarTexto()
        {
            txtCronometro.text   = $"Cronómetro: {tiempo.Cronometrar():00:00.00}";
            txtTemporizador.text = $"Temporizador: {tiempo.Temporizar(10):00:00.00}";
        }
    }
}

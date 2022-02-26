using UnityEngine;

namespace JugadorCharacterController
{
    [DisallowMultipleComponent]
    public class JugadorEntrada : JugadorComponenteBase
    {
        #region Atributos
        internal bool Esta_Espacio_Presionandose;
        internal bool Esta_ClickIzquierdo_Presionado;
        internal bool Esta_ClickCentral_Presionandose;
        internal bool Esta_ClickDerecho_Presionandose;
        #endregion

        #region Eventos
        private void Update()
        {
            jugador.Movimiento.x = Input.GetAxisRaw("Horizontal");
            jugador.Movimiento.z = Input.GetAxisRaw("Vertical");

            Esta_Espacio_Presionandose      = Input.GetKeyDown(KeyCode.Space);
            Esta_ClickIzquierdo_Presionado  = Input.GetMouseButtonDown(0);
            Esta_ClickDerecho_Presionandose = Input.GetMouseButton(1);
            Esta_ClickCentral_Presionandose = Input.GetMouseButton(2);

            jugador.Movimiento.ratonX = Input.GetAxis("Mouse X");
        }
        #endregion
    }
}

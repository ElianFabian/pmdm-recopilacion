using UnityEngine;

namespace JugadorCharacterController
{
    [DisallowMultipleComponent]
    class JugadorColision : JugadorComponenteBase
    {
        #region Atributos
        internal bool EstaEnSuelo;

        const string TAG_SUELO = "Suelo";  // TODO: *** CREAR ETIQUETA SUELO (si no existe ya) ***
        #endregion

        #region Eventos
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.collider.CompareTag(TAG_SUELO)) return;

            EstaEnSuelo = true;
        }
        #endregion
    }
}
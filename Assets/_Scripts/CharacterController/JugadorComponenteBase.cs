using UnityEngine;

namespace JugadorCharacterController
{
    public abstract class JugadorComponenteBase : MonoBehaviour
    {
        protected Jugador jugador;

        protected virtual void Start()
        {
            jugador = GetComponent<Jugador>();
        }
    }
}
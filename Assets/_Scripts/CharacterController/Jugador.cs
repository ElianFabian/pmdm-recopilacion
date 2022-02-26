using UnityEngine;

namespace JugadorCharacterController
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public class Jugador : MonoBehaviour
    {
        #region Atributos
        [SerializeField] internal Camera Camara;
        [SerializeField] internal Bala Bala;
        [SerializeField][Range(0, 5)]   internal float DesfaseDistanciaBala = 0;
        [SerializeField][Range(-5, 5)]  internal float DesfaseAlturaBala    = 0.35f;
        [SerializeField][Range(0, 100)] internal byte  TasaDeDisparo        = 20;
        [SerializeField][Range(0, 60)]  internal float velocidadMaxima_XZ   = 10;
        [SerializeField][Range(0, 50)]  internal float suavidadGiro         = 20;
        [SerializeField][Range(0, 100)] internal float alturaMaxima         = 2;

        #region Lógica
        internal JugadorEntrada Entrada;
        internal JugadorColision Colision;
        internal JugadorMovimiento Movimiento;
        internal JugadorAccion Accion;
        #endregion

        #region Constantes
        internal const float GRAVEDAD         = -9.81f;
        #endregion

        internal CharacterController controlador;
        #endregion

        #region Eventos
        void Awake()
        {
            Entrada    = GetComponent<JugadorEntrada>();
            Movimiento = GetComponent<JugadorMovimiento>();
            Colision   = GetComponent<JugadorColision>();
            Accion     = GetComponent<JugadorAccion>();

            controlador = GetComponent<CharacterController>();
            
            // Si no se añade una cámara desde el inspector se utilizará la main
            if (Camara == null) Camara = Camera.main;
        }
        #endregion
    }
}
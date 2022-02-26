using UnityEngine;

namespace JugadorCharacterController
{
    [DisallowMultipleComponent]
    class JugadorMovimiento : JugadorComponenteBase
    {
        #region Atributos
        internal Vector3 Velocidad = new Vector3();
        internal bool UsarGravedad = true;
        internal float x;
        internal float z;
        internal float ratonX;
        internal float VelocidadExtra = 5;
        #endregion

        #region Eventos
        private void FixedUpdate()
        {
            if (UsarGravedad) AplicarGravedad();

            Mover();

            if (EstaMoviendose) Rotar();
        }
        #endregion

        #region Métodos
        void Mover()
        {
            var velocidadExtra = 0.0f;
            if (jugador.Entrada.Esta_ClickDerecho_Presionandose) velocidadExtra = VelocidadExtra;

            // Velocidad caminando
            Velocidad.x = Direccion.x * (jugador.velocidadMaxima_XZ + velocidadExtra);
            Velocidad.z = Direccion.z * (jugador.velocidadMaxima_XZ + velocidadExtra);

            jugador.controlador.Move(Velocidad * Time.fixedDeltaTime); 
        }
        void Rotar()
        {
            // El jugador mira en el sentido del movimiento
            var rotacion = Quaternion.LookRotation(Direccion);

            // Suaviza el giro
            jugador.transform.rotation = Quaternion.Slerp
            (
                jugador.transform.rotation,
                rotacion,
                jugador.suavidadGiro * Time.fixedDeltaTime
            );
        }
        void AplicarGravedad()
        {
            Velocidad.y += Jugador.GRAVEDAD * Time.fixedDeltaTime;

            if (jugador.controlador.isGrounded && Velocidad.y < 0)
            {
                Velocidad.y = 0;
            }
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Devuelve el vector de movimiento del jugador respecto de la cámara.
        /// </summary>
        internal Vector3 Direccion
        {
            get
            {
                var camaraActual = jugador.Camara;

                // Se obtienen los vectores de derecha y delante de la cámara
                var CamaraDerecha = camaraActual.transform.right;
                var CamaraArriba  = camaraActual.transform.up;      // Se usará up en lugar de forward si la cámara mira perpendicularmente al suelo/techo
                var CamaraDelante = camaraActual.transform.forward;

                // Se pone a 0 la componente Y ya que sólo nos interesa mover al jugador en los ejes XZ
                CamaraDerecha.y = 0;
                CamaraArriba.y  = 0;
                CamaraDelante.y = 0;

                CamaraDerecha = CamaraDerecha.normalized;
                CamaraArriba  = CamaraArriba.normalized;
                CamaraDelante = CamaraDelante.normalized;

                var angulo = Vector3.Angle(camaraActual.transform.forward, Vector3.up);

                // Se devuelve el vector correspondiente según la orientación de la cámara
                return angulo == 0 || angulo == 180
                ?
                (CamaraDerecha*x + CamaraArriba*z).normalized
                :
                (CamaraDerecha*x + CamaraDelante*z).normalized;
            }
            private set { }
        }
        internal bool EstaMoviendose { get => Direccion != Vector3.zero; }
        #endregion
    }
}

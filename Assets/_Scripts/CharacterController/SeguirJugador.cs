using UnityEngine;

namespace JugadorCharacterController
{
    [DisallowMultipleComponent]
    public class SeguirJugador : MonoBehaviour
    {
        #region Atributos
        [SerializeField] Jugador Objetivo;

        [SerializeField] bool MirarAObjetivo = true;
        [SerializeField] bool SeguirObjetivo = true;
        [SerializeField] bool RotarConRaton  = true;
        [SerializeField] bool AjustarAltura  = true;

        [SerializeField][Range(0, 50)] float VelocidadDeAjusteDeAltura = 5;
        [SerializeField][Range(0, 10)] float Suavidad                  = 0.2f; // Modifica la suavidad con la que la cámara sigue al objeto

        [SerializeField] Vector2 VelocidadRaton = new Vector2(5, 2);
        [SerializeField] Vector3 Desfase; // Distancia entre el jugador y la cámara

        float DesfaseMagnitudInicial;
        float DesfaseYInicial;

        Vector3 VelocidadObjetivo;
        #endregion

        #region Eventos
        private void Start()
        {
            Desfase                = transform.position - Objetivo.transform.position;
            DesfaseMagnitudInicial = Desfase.magnitude;
            DesfaseYInicial        = transform.position.y - Objetivo.transform.position.y;
            VelocidadObjetivo      = Objetivo.Movimiento.Velocidad;
        }

        private void LateUpdate()
        {
            if (RotarConRaton) MoverCamaraConRaton();
        }

        private void FixedUpdate()
        {
            if (AjustarAltura) AjustarAlturaRelativa();
            if (SeguirObjetivo) Seguir();
        }
        #endregion

        #region Métodos
        void Seguir()
        {
            var nuevaPosicion = Objetivo.transform.position + Desfase;

            transform.position = Vector3.SmoothDamp
            (
                current: transform.position,
                target: nuevaPosicion,
                currentVelocity: ref VelocidadObjetivo,
                smoothTime: Suavidad
            );

            if (MirarAObjetivo) transform.LookAt(Objetivo.transform);
        }

        void MoverCamaraConRaton()
        {
            //var ratonX = Input.GetAxis("Mouse X");
            var ratonX = Objetivo.Movimiento.ratonX;
            //var ratonY = Input.GetAxis("Mouse Y");

            transform.RotateAround(Objetivo.transform.position, Vector2.up, ratonX * VelocidadRaton.x);
            //transform.RotateAround(Objetivo.position, Vector2.right, ratonY * VelocidadRaton.y);

            #region Limitar movimiento vertical de la cámara (no funciona)
            //var rotacion = transform.rotation.eulerAngles;

            //if (0 <= rotacion.x && rotacion.x <= 50)
            //{
            //  transform.RotateAround(Objetivo.position, Vector2.right, ratonY * VelocidadRaton.y);
            //}

            //rotacion.x = Mathf.Clamp(rotacion.x, 0, 50);

            //transform.rotation = Quaternion.Euler(rotacion);
            #endregion

            // Si el jugador se mueve se cambia el desfase para que la cámara no esté siempre
            // en la misma posición relativa (para que pueda rotar)
            if (!Objetivo.Movimiento.EstaMoviendose || Objetivo.Movimiento.EstaMoviendose && Mathf.Abs(ratonX) > 0)
            {
                Desfase = transform.position - Objetivo.transform.position;
                Desfase = Desfase.normalized * DesfaseMagnitudInicial;
            }
        }

        /// <summary>
        /// La altura relativa de la cámara respecto del jugador siempre será la misma.
        /// </summary>
        void AjustarAlturaRelativa()
        {
            transform.position = Vector3.Lerp
            (
                transform.position,
                new Vector3
                (
                    transform.position.x,
                    Objetivo.transform.position.y + DesfaseYInicial,
                    transform.position.z
                ),
                VelocidadDeAjusteDeAltura * Time.deltaTime
            );
        }
        #endregion
    }
}
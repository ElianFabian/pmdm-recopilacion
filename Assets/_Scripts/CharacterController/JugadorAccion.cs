using UnityEngine;

namespace JugadorCharacterController
{
    [DisallowMultipleComponent]
    class JugadorAccion : JugadorComponenteBase
    {
        #region Atributos
        float SiguienteVezParaDisparar;
        #endregion

        #region Eventos
        protected override void Start()
        {
            base.Start();

            SiguienteVezParaDisparar = 0;
        }
        private void LateUpdate()
        {
            EjecutarAcciones();
        }
        #endregion

        #region Métodos
        void EjecutarAcciones()
        {
            #region Saltar
            if (jugador.Entrada.Esta_Espacio_Presionandose && jugador.Colision.EstaEnSuelo)
            {
                Saltar(jugador.alturaMaxima);
            }
            #endregion

            #region Disparo normal
            if (jugador.Entrada.Esta_ClickIzquierdo_Presionado)
            {
                Disparar(jugador.Bala);
            }
            #endregion

            #region Disparo de ráfaga
            if (jugador.Entrada.Esta_ClickCentral_Presionandose && Time.time >= SiguienteVezParaDisparar)
            {
                SiguienteVezParaDisparar = Time.time + 1/(float)jugador.TasaDeDisparo;
                Disparar(jugador.Bala);
            }
            #endregion
        }

        void Saltar(float altura = 2)
        {
            // Fórmula física que obtiene la velocidad necesaria para saltar a una determinada altura
            jugador.Movimiento.Velocidad.y = Mathf.Sqrt(-2 * Jugador.GRAVEDAD * altura);

            jugador.Colision.EstaEnSuelo = false;
        }
        void Disparar(Bala bala)
        {
            var desfaseAltura    = transform.up      * jugador.DesfaseAlturaBala;
            var desfaseDistancia = transform.forward * jugador.DesfaseDistanciaBala;

            var nuevaBala = Instantiate
            (
                bala,
                transform.position + desfaseAltura + desfaseDistancia,
                transform.rotation
            );
            nuevaBala.Disparar();
        }
        #endregion
    }
}
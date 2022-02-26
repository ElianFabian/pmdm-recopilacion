using UnityEngine;

namespace JugadorCharacterController
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody))]
    public class Bala : MonoBehaviour
    {
        #region Atributos
        [SerializeField] float Velocidad    = 30;
        [SerializeField] float TiempoDeVida = 5;
        [SerializeField] bool  UsarGravedad = false;
        [SerializeField] bool  IsTrigger    = true;

        bool destruir = false;
        float tiempoDeAudioReproducido = 0;

        AudioSource Sonido;

        const string TAG_COLISIONABLE = "Colisionable"; // TODO: *** CREAR TAG COLISIONABLE (si no existe ya) ***
        #endregion

        #region Eventos
        private void Awake()
        {
            Sonido = GetComponent<AudioSource>();

            // Configuración
            GetComponent<Rigidbody>().useGravity = UsarGravedad;
            GetComponent<Collider>().isTrigger   = IsTrigger;
        }
        private void Update()
        {
            // Después de colisionar se destruye la bala cuando ya se ha reproducido su sonido
            if (destruir) tiempoDeAudioReproducido += Time.deltaTime;
            if (!destruir || tiempoDeAudioReproducido < Sonido.clip.length) return;

            Destroy(gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(TAG_COLISIONABLE)) return;

            // Cuando colisiona se encoge la bala a 0
            // para que antes de destruirla le de tiempo a reproducir su sonido
            transform.localScale = Vector3.zero;

            destruir = true;
        }
        #endregion

        #region Métodos
        public void Disparar()
        {
            transform.rotation *= Quaternion.Euler(90, 0, 0); // Se rota para que la cabeza de la bala mire en sentido del movimiento

            transform.GetComponent<Rigidbody>().AddForce
            (
                force: transform.up * Velocidad,
                mode: ForceMode.Impulse
            );
            Destroy(gameObject, TiempoDeVida);
        }
        #endregion
    }
}
using UnityEngine;

namespace LerpYPinPong
{
    public class SubirYBajar_Seno : MonoBehaviour
    {
        [SerializeField][Range(0, 20)] float Amplitud = 15;
        [SerializeField][Range(0, 20)] float Velocidad = 4;
        float AlturaInicial;

        private void Start()
        {
            AlturaInicial = transform.position.y;
        }

        private void Update()
        {
            SubirYBajar(Amplitud, Velocidad);
        }

        void SubirYBajar(float amplitud, float velocidad)
        {
            var varicion = amplitud * Mathf.Sin(velocidad * Time.time);
            var desfaseParaCentrar = amplitud / 2;

            transform.position = new Vector3
            (
                transform.position.x,
                AlturaInicial + varicion - desfaseParaCentrar,
                transform.position.z
            );
        }
    }

}
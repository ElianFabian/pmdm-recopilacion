using UnityEngine;
using UnityEngine.UI;

namespace LerpYPinPong
{
    public class Controlador : MonoBehaviour
    {
        [SerializeField] Slider sldPosicionPunto;
        [SerializeField] Transform Origen;
        [SerializeField] Transform Destino;
        [SerializeField] Transform Punto;

        private void Update()
        {
            EstablecerPosicionPunto();
        }

        public void EstablecerPosicionPunto()
        {
            Punto.position = Vector3.Lerp
            (
                a: Origen.position,
                b: Destino.position,
                t: sldPosicionPunto.value
            );
        }
    }
}

using UnityEngine;

namespace DestuirObjectosEmitidosConClick
{
    public class EmisorDeObjetos : MonoBehaviour
    {
        [SerializeField] Rigidbody Objeto;
        [SerializeField] float Velocidad = 5;
        [SerializeField] byte TasaDeEmision = 2;

        float SiguienteVezParaDisparar = 0;

        private void Update()
        {
            if (Time.time >= SiguienteVezParaDisparar)
            {
                var objetoInstanciado      = Instantiate(Objeto, transform.position, Quaternion.identity);
                objetoInstanciado.velocity = Random.onUnitSphere * Velocidad;

                SiguienteVezParaDisparar = Time.time + 1/(float)TasaDeEmision;
            }
        }
    }
}
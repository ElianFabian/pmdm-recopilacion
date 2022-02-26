using UnityEngine;

namespace LerpYPinPong
{
    public class SubirYBajar_PingPong : MonoBehaviour
    {
        [SerializeField][Range(0, 20)] float Amplitud = 15;
        [SerializeField][Range(0, 20)] float Velocidad = 5;
        float AlturaInicial;

        private void Start()
        {
            AlturaInicial = transform.position.y;
        }

        private void Update()
        {
            var varicion = Mathf.PingPong(Velocidad * Time.time, Amplitud);
            var desfaseParaCentrar = Amplitud / 2;

            transform.position = new Vector3
            (
                transform.position.x,
                AlturaInicial + varicion - desfaseParaCentrar,
                transform.position.z
            );
        }
    }

}
using UnityEngine;

namespace Corrutinas
{
    public class Cubo : MonoBehaviour
    {
        public bool EstaMoviendose = false;
        public bool EstaRotando = false;


        private void Update()
        {
            if (EstaMoviendose) Mover();
            if (EstaRotando) Rotar();
        }

        void Mover()
        {
            transform.Translate(6 * Time.deltaTime * Vector3.forward, Space.World);
        }
        void Rotar()
        {
            transform.rotation *= Quaternion.Euler(100 * Time.deltaTime * transform.up);
        }
    }
}


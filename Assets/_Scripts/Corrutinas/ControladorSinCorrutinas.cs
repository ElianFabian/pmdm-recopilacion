using UnityEngine;
using UnityEngine.UI;

using UnityUtils;

namespace Corrutinas
{
    public class ControladorSinCorrutinas : MonoBehaviour
    {
        [SerializeField] Transform Cubos;
        [SerializeField] Text txtTiempo;

        bool boton = false;

        readonly Tareas tareas = new Tareas();

        private void Start()
        {
            AniadirTareasALista();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) boton = !boton;

            var tiempo = Time.time;
            txtTiempo.text = $"Tiempo: {tiempo:00:00.00}";

            // Si bot�n est� a true las acciones se ir�n ejecutando de una a una
            // cuando sea false las restantes se quedar�n pendientes hasta que vuelva a ser true
            if (boton) tareas.Update(tiempoRelativo: true);
        }

        void AniadirTareasALista()
        {
            // Los cubos se ir�n desplazando hacia delante de uno en uno
            foreach (var cubo in Cubos.GetComponentsInChildren<Cubo>())
            {
                tareas.AniadirRelativo(0.25f, () => cubo.EstaMoviendose = true);
            }

            // Luego rotar�n de uno en uno
            foreach (var cubo in Cubos.GetComponentsInChildren<Cubo>())
            {
                tareas.AniadirRelativo(0.25f, () => cubo.EstaRotando = true);
            }

            // Habr� una espera de un segundo
            tareas.AniadirRelativo(1, () => { });

            // Detiene los cubos de uno en uno
            foreach (var cubo in Cubos.GetComponentsInChildren<Cubo>())
            {
                tareas.AniadirRelativo(0.25f, () =>
                {
                    cubo.EstaRotando = false;
                    cubo.EstaMoviendose = false;
                });
            }

            // Se dentendr�n todos a la vez
            //foreach (var cubo in Cubos.GetComponentsInChildren<Cubo>())

            //    tareas.AniadirRelativo(0, () =>
            //    {
            //        cubo.EstaRotando = false;
            //        cubo.SeEstaMoviendo = false;
            //    });
            //}
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace Corrutinas
{
    public class ControladorConCorrutinas : MonoBehaviour
    {
        [SerializeField] Transform Cubos;
        [SerializeField] Text txtTiempo;

        private void Start()
        {
            StartCoroutine(Co_General());
        }

        private void Update()
        {
            var tiempo = Time.time;
            txtTiempo.text = $"Tiempo: {tiempo:00:00.00}";
        }

        IEnumerator Co_General()
        {
            yield return StartCoroutine(Co_MoverCubos());
            yield return StartCoroutine(Co_RotarCubos());

            yield return new WaitForSeconds(1f);

            yield return Co_DetenerCubos();
        }
        IEnumerator Co_MoverCubos()
        {
            foreach (var cubo in Cubos.GetComponentsInChildren<Cubo>())
            {
                cubo.EstaMoviendose = true;
                yield return new WaitForSeconds(0.25f);
            }
        }
        IEnumerator Co_RotarCubos()
        {
            foreach (var cubo in Cubos.GetComponentsInChildren<Cubo>())
            {
                cubo.EstaRotando = true;
                yield return new WaitForSeconds(0.25f);
            }
        }
        IEnumerator Co_DetenerCubos()
        {
            foreach (var cubo in Cubos.GetComponentsInChildren<Cubo>())
            {
                cubo.EstaMoviendose = false;
                cubo.EstaRotando = false;
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
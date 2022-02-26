using UnityEngine;
using UnityEngine.AI;

namespace InteligenciaArtificial
{
    class IrADondeSeHayaClickeado : MonoBehaviour
    {
        [SerializeField] Camera Camara;
        [SerializeField] Transform luzDestino;

        NavMeshAgent Agente;

        private void Start()
        {
            Agente = GetComponent<NavMeshAgent>();

            if (Camara == null) Camara = Camera.main;
        }

        private void Update()
        {
            var rayo = Camara.ScreenPointToRay(Input.mousePosition);

            var mouse0Down = Input.GetKeyDown(KeyCode.Mouse0);

            if (mouse0Down && Physics.Raycast(rayo, out RaycastHit hit))
            {
                Agente.destination = hit.point;

                luzDestino.position = hit.point + 0.75f * Vector3.up;
            }
        }
    }
}

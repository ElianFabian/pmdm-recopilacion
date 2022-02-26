using UnityEngine;
using UnityEngine.AI;

namespace InteligenciaArtificial
{
    public class IrADestino : MonoBehaviour
    {
        [SerializeField] Transform Destino;

        NavMeshAgent Agente;

        private void Start()
        {
            Agente = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            Agente.destination = Destino.position;
        }
    }
}
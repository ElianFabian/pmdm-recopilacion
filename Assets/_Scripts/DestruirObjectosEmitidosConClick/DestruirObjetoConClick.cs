using UnityEngine;

namespace DestuirObjectosEmitidosConClick
{
    class DestruirObjetoConClick : MonoBehaviour
    {
        [SerializeField] Camera Camara;

        private void Start()
        {
            if (Camara == null) Camara = Camera.main;
        }

        private void Update()
        {
            var click = Input.GetKey(KeyCode.Mouse0);

            var rayo = Camara.ScreenPointToRay(Input.mousePosition);

            if (click && Physics.Raycast(rayo, out RaycastHit hit))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}

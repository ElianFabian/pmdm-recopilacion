using UnityEngine;

public class AlternarCamaras : MonoBehaviour
{
    [SerializeField] Transform[] Camaras;

    int indiceCamaraActiva = 0;

    private void Start()
    {
        Camaras = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            Camaras[i] = transform.GetChild(i);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SiguienteCamara();
        }
    }

    public void SiguienteCamara()   
    {
        Camaras[indiceCamaraActiva].gameObject.SetActive(false);

        indiceCamaraActiva = ++indiceCamaraActiva % Camaras.Length;

        Camaras[indiceCamaraActiva].gameObject.SetActive(true);
    }
}

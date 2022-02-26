using UnityEngine;

public class ApuntarAObjetivo : MonoBehaviour
{
    [SerializeField] Transform otherObject;

    private void Update()
    {
        transform.LookAt(otherObject);
        transform.Rotate(Vector3.right * 90);
    }
}

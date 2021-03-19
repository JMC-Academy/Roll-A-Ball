using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject particlePrefab;

    public void CreateParticles()
    {
        Instantiate(particlePrefab, transform.position, transform.rotation);
    }
}

using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedef;
    public float mesafe = 5f;
    public float yukseklik = 3f;
    public float yumusaklik = 5f;

    void LateUpdate()
    {
        if (hedef == null) return;

        // karakterin arkasında ve biraz yukarıda dur
        Vector3 gidilecekYer = hedef.position - hedef.forward * mesafe + Vector3.up * yukseklik;
        transform.position = Vector3.Lerp(transform.position, gidilecekYer, yumusaklik * Time.deltaTime);
        transform.LookAt(hedef.position + Vector3.up * 1.5f);
    }
}

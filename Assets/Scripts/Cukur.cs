using UnityEngine;

public class Cukur : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Top"))
        {
            other.tag = "Untagged"; // iki kere sayılmasın
            OyunYonetici.instance.TopDustu();
            Destroy(other.gameObject, 3f);
        }
    }
}

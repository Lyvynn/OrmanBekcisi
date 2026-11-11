using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class OyunYonetici : MonoBehaviour
{
    public static OyunYonetici instance;

    public float sure = 60f;
    public OyuncuHareket oyuncu;
    public GameObject[] toplar;
    public Transform[] cukurlar;

    public Text sureText;
    public Text skorText;
    public Text mesajText;
    public GameObject bitisPaneli;
    public Text bitisText;

    int skor = 0;
    bool oyunBitti = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bitisPaneli.SetActive(false);

        // topları haritaya rastgele dağıt
        foreach (GameObject top in toplar)
        {
            top.transform.position = RastgeleYer();
        }

        mesajText.text = "Kötü büyücü ormandaki sihirli topları dağıttı!\n60 saniye içinde 10 topu da çukurlara at!";
        Invoke("MesajiKapat", 5f);
        YazilariGuncelle();
    }

    void Update()
    {
        if (oyunBitti)
        {
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        sure -= Time.deltaTime;
        if (sure <= 0)
        {
            sure = 0;
            OyunuBitir("SÜRE BİTTİ!");
        }
        YazilariGuncelle();
    }

    public void TopDustu()
    {
        if (oyunBitti) return;
        skor++;
        YazilariGuncelle();

        if (skor >= toplar.Length)
            OyunuBitir("KAZANDINIZ!");
    }

    void OyunuBitir(string yazi)
    {
        oyunBitti = true;
        oyuncu.oynayabilir = false;
        bitisPaneli.SetActive(true);
        bitisText.text = yazi + "\n\nÇukura giren top: " + skor + " / " + toplar.Length + "\n\nTekrar oynamak için R tuşuna bas";
    }

    void YazilariGuncelle()
    {
        sureText.text = "Süre: " + Mathf.CeilToInt(sure);
        skorText.text = "Çukurdaki top: " + skor + "/" + toplar.Length + "   Kalan: " + (toplar.Length - skor);
    }

    void MesajiKapat()
    {
        mesajText.text = "";
    }

    Vector3 RastgeleYer()
    {
        Vector3 yer = Vector3.zero;
        for (int i = 0; i < 100; i++)
        {
            yer = new Vector3(Random.Range(-20f, 20f), 0.5f, Random.Range(-20f, 20f));

            bool uygun = true;
            foreach (Transform c in cukurlar)
            {
                if (Vector3.Distance(new Vector3(c.position.x, 0.5f, c.position.z), yer) < 4f) uygun = false;
            }
            if (Vector3.Distance(oyuncu.transform.position, yer) < 3f) uygun = false;
            if (Physics.CheckSphere(yer + Vector3.up * 0.6f, 0.5f)) uygun = false; // ağaç taş vs

            if (uygun) break;
        }
        return yer;
    }
}

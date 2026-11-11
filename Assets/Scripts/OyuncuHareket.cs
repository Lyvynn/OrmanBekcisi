using UnityEngine;
using UnityEngine.InputSystem;

public class OyuncuHareket : MonoBehaviour
{
    public float hiz = 4f;
    public float donmeHizi = 150f;
    public Animator anim;
    public bool oynayabilir = true;

    Rigidbody rb;
    Vector3 baslangicNoktasi;
    float ileri;
    float donus;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baslangicNoktasi = transform.position;
    }

    void Update()
    {
        ileri = 0;
        donus = 0;

        if (oynayabilir && Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) ileri = 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) ileri = -1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) donus = -1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) donus = 1;
        }

        // yürüyorsa walk animasyonu, durunca idle
        if (anim != null)
            anim.SetBool("yuruyor", ileri != 0);

        // çukura düşerse başa dön
        if (transform.position.y < -5)
        {
            transform.position = baslangicNoktasi;
            rb.linearVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, donus * donmeHizi * Time.fixedDeltaTime, 0));

        Vector3 hareket = transform.forward * ileri * hiz;
        hareket.y = rb.linearVelocity.y;
        rb.linearVelocity = hareket;
    }
}

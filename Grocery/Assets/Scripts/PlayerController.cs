using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;

    private float horizontalInput;
    private float verticalInput;
    public float speed = 10;
    public float turn_speed = 100;
    private String eldeki_silah;
    private int mermi_sayisi;
    private int silah_index;
    public Transform firePoint;

    public TextMeshProUGUI inventoryText;

    public GameObject GunsInventory;

    private Dictionary<string,
    int> Backpack = new Dictionary<string,
    int>();

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        emptyBackpack();
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        RotateMousePosition();
        shot();
        SwapGun();
    }

    void MoveCharacter()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
    }

    void RotateMousePosition()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // niyeyse sprite tam mouseye doğru dönmedi. ekstra -90 derece ekliyorum.
        rotation *= Quaternion.Euler(0, 0, -90);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("ekmek"))
        {
            Backpack["ekmek-mermi"] += 1;
            Destroy(collision.gameObject);
            countInventory();
        }
        else if (collision.transform.CompareTag("ketcap"))
        {
            Backpack["ketcap-mermi"] += 1;
            Destroy(collision.gameObject);
            countInventory();
        }
    }

    void emptyBackpack()
    {
        Backpack.Add("ekmek-mermi", 0);
        Backpack.Add("ketcap-mermi", 0);
        eldeki_silah = Backpack.ElementAt(0).Key;
        mermi_sayisi = Backpack.ElementAt(0).Value;
        silah_index = 0;
    }

    void countInventory()
    {
        Debug.Log("collision");
        // looplarda concat ancak bununla yapabildim
        var sb = new System.Text.StringBuilder();
        foreach (KeyValuePair<string, int> i in Backpack)
        {
            if (i.Value > 0)
            {
                if (i.Key == eldeki_silah)
                {
                    sb.AppendLine(i.Key.Split('-')[0] + " : " + i.Value.ToString() + "(Kullanımda)");
                }
                else
                {
                    sb.AppendLine(i.Key.Split('-')[0] + " : " + i.Value.ToString());
                }

            }
            else if (i.Value == 0)
            {
                if (i.Key == eldeki_silah)
                {
                    sb.AppendLine(i.Key.Split('-')[0] + " : " + "Şarjör Boş (Kullanımda)");
                }
                else
                {
                    sb.AppendLine(i.Key.Split('-')[0] + " : " + "Şarjör Boş");
                }

            }

        }

        inventoryText.text = sb.ToString();

    }

    void shot()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Backpack.ElementAt(silah_index).Value > 0)
            {

                foreach (Transform child in GunsInventory.transform) if (child.CompareTag(eldeki_silah))
                    {
                        shotBullet(child.gameObject, eldeki_silah);

                    }
            }

        }
    }

    void shotBullet(GameObject child, String EldekiSilah)
    {
        Instantiate(child.gameObject, firePoint.position, firePoint.rotation);
        Backpack[EldekiSilah] -= 1;
        countInventory();
    }

    void SwapGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f || Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if (silah_index < Backpack.Count - 1)
            {
                silah_index++;
                eldeki_silah = Backpack.ElementAt(silah_index).Key;
                mermi_sayisi = Backpack.ElementAt(silah_index).Value;
                countInventory(); // kullanımda textinin güncellenmesi icin
            }
            else
            {
                silah_index = 0;
                eldeki_silah = Backpack.ElementAt(silah_index).Key;
                mermi_sayisi = Backpack.ElementAt(silah_index).Value;
                countInventory(); // kullanımda textinin güncellenmesi iciin
            }

        }
    }

}

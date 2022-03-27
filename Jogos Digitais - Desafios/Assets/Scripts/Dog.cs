using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    // Start is called before the first frame update

    public bool vindoDireita;
    public Rigidbody2D rigid;
    public gameManager managerLink;

    void Start()
    {
        managerLink = GameObject.Find("gameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!vindoDireita)
            {
                rigid.velocity = transform.TransformDirection(Vector2.left * managerLink.inimigoVelocidade);
            }
            else
            {
                rigid.velocity = transform.TransformDirection(Vector2.right * managerLink.inimigoVelocidade);
            }
            
            Destroy(gameObject, 20);
        }
    }


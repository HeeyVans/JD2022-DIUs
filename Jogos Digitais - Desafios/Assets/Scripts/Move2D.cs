using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public int moveSpeed;
    private float direction;

    private Vector3 facingRight;
    private Vector4 facingLeft;

    public bool taNoChao;
    public Transform detactaChao;
    public LayerMask oQueEChao;
    public Animator anime;
    public Rigidbody2D bola;
    public Transform spawnBola;

    public gameManager managerLink;

    float reloadTime;

    public Image reloadBar;

    public AudioSource somGato;

    
    // Start is called before the first frame update
    void Start()
    {
        managerLink = GameObject.Find("gameManager").GetComponent<gameManager>();
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        taNoChao = Physics2D.OverlapCircle(detactaChao.position, 0.2f, oQueEChao);
        anime.SetBool("noChao", taNoChao);
        UpdateBar();

        if(reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }

        if (direction != 0)
        {
            anime.SetBool("seMovendo", true);
        }
        else{
            anime.SetBool("seMovendo", false);
        }


        if(Input.GetButtonDown("Jump") && taNoChao == true)
        {
            rb.velocity = Vector2.up * 15; // o número multiplicado resultará em um pulo mais longo do personagem.
        }

        direction = Input.GetAxis("Horizontal"); //Todo momento verificando a direção e guardando o que o usuário digita no teclado dentro da variável.

        if(direction > 0)
        {
            //olhando para a direita
            transform.localScale = facingRight;
        }

        if(direction < 0){

            //olhando para a esquerda
            transform.localScale = facingLeft;
        }

        rb.velocity = new Vector2(direction*moveSpeed, rb.velocity.y); //Valor positivo no X ele vai para direita e valor negativo vai para a esquerda

        atirarBola();
    }

    void atirarBola(){

        if(Input.GetButtonDown("Fire1") && reloadTime <= 0){
            Rigidbody2D clone = Instantiate(bola, spawnBola.position, transform.rotation);

            if(transform.localScale.x < 0 )
            {
                clone.velocity = transform.TransformDirection(Vector2.left * 10);
            }
            else if (transform.localScale.x > 0)
            {
                clone.velocity = transform.TransformDirection(Vector2.right * 10);
            }
            
            somGato.Play();
            reloadTime = 3;
            Destroy(clone, 5);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("inimigo")){
           // Destroy(other.gameObject);
            //Destroy(gameObject, 0.05f);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            managerLink.GameOver();
        }
    }

    void UpdateBar(){
        reloadBar.fillAmount = reloadTime / 3;
    }

}

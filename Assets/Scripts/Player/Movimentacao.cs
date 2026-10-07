using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    [Header("Movimentacao")]
    public float velocidadeMaxima = 8f;
    public float aceleracao = 5f;
    public float desaceleracao = 8f;
    [Header("Pulo")]
    public float forcaPulo = 10f;
    public float multiplicadorQueda = 2.5f;
    public float multiplicadorPuloCurto = 2f;
    [Header("Chao Check")]
    public Transform verificarChao;
    public float raiochao = 0.2f;
    public LayerMask layerChao;
    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator anim;
    [Header("Pausar")]
    public GameObject painelPause;
    private bool pausado;

    private float direcao;
    private bool noChao;
    private bool olhandoDireita = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        direcao = Input.GetAxisRaw("Horizontal");

        VerificarChao();
        Virar();
        Pausar();
        Pular();
    }
    void FixedUpdate()
    {
        Mover();
        MelhorarGravidade();

    }
    private void Mover()
    {
        float velocidadeDesejada = direcao * velocidadeMaxima;
        rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, velocidadeDesejada, (direcao != 0 ? aceleracao : desaceleracao) * Time.fixedDeltaTime), rb.linearVelocity.y);
    }
    private void Pular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }
    }
    private void MelhorarGravidade()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (multiplicadorQueda - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (multiplicadorPuloCurto - 1) * Time.fixedDeltaTime;
        }

    }
    private void VerificarChao()
    {
        noChao = Physics2D.OverlapCircle(verificarChao.position, raiochao, layerChao);
    }
    private void Virar()
    {
        if (direcao > 0 && !olhandoDireita)
        {
            Inverter();
        }
        else if (direcao < 0 && olhandoDireita)
        {
            Inverter();
        }
    }
    private void Inverter()
    {
        olhandoDireita = !olhandoDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(verificarChao.position, raiochao);
    }
    public void Pausar()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausado = !pausado;
            painelPause.SetActive(pausado);
            Time.timeScale = pausado ? 0f : 1f;
        }
        if (pausado && Input.GetKeyDown(KeyCode.Escape))
        {
            pausado = false;
            painelPause.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

using UnityEngine;

public class mouvements : MonoBehaviour
{
    //player's speed and display
    [SerializeField] private float speed;
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float scale;

    //mvs limits
    [SerializeField] private GameObject TopRight;
    [SerializeField] private GameObject ButtomLeft;
    private Vector3 TopRightV;
    private Vector3 ButtomLeftV;
    
    //player's status
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    // Initialsation
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        TopRightV = TopRight.transform.position;
        ButtomLeftV = ButtomLeft.transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when facing left/right.
        if (horizontalInput < -0.01f)
            transform.localScale = Vector3.one*scale;
        else if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1)*scale;

        //OnCollisionEnter2D(collision);
        if (Input.GetKey(KeyCode.Space) && grounded) Jump();

        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    //}
    // Called after Update
    //private void LateUpdate()
    //{
        //Vector3 CurrentPosition = body.transform.position;
        //CurrentPosition.x = Mathf.Clamp(CurrentPosition.x,TopRightV.x,ButtomLeftV.x);
        //CurrentPosition.y = Mathf.Clamp(CurrentPosition.y, TopRightV.y, ButtomLeftV.y);
        //body.transform.position = CurrentPosition;
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, JumpSpeed);
        grounded = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") grounded = true;
    }
    
    public void MoveForward()
    {
        body.velocity = new Vector2(speed, body.velocity.y);


    }
}

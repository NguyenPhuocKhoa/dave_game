using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float _xInput, _yInput;
    private bool _canDoubleJump;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigid2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] public AudioSource _audio;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip _fallSound;
    [SerializeField] public AudioClip _runSound;    
    [SerializeField] private CanvasController _canvasCtrl;
    [SerializeField] private Camera _camera;

    [Header("Collision")]
    private bool _onGround = true;
    private bool _onWall = false;
    public Vector2 _rayLength;

    [Header("Components")]
    public LayerMask _groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("isIdle", true);
        _animator.SetBool("onGround", _onGround);
        _animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetBool("isDead")) return;

        if (_rigid2D.velocity.y < 0 && !_onGround)
        {
            _animator.SetBool("isFall", true);
            _audio.PlayOneShot(_fallSound);
        }
        else
        {
            _animator.SetBool("isFall", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_onGround)
            {
                jump();
                _canDoubleJump = true;
                if(_audio && jumpSound)
                {
                    _audio.PlayOneShot(jumpSound);
                }
            }
            else if (_canDoubleJump)
            {
                _jumpForce /= 1.2f;
                jump();
                _jumpForce *= 1.2f;
                _canDoubleJump = false;
                if (_audio && jumpSound)
                {
                    _audio.PlayOneShot(jumpSound);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_animator.GetBool("isDead")) return;
        move();
        FlipPlayer();
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _rayLength.y, _groundLayer);
        _onWall = Physics2D.OverlapCircle(_groundCheck.position, _rayLength.x * 2, _groundLayer);
        _animator.SetBool("onGround", _onGround);
       
    }

    void move()
    {
        // Giam toc do di chuyen theo chieu x de Player khong bi bam tuong, nhung khong bi dung lai.
        _xInput = _onWall && !_onGround ? Input.GetAxis("Horizontal") / 3 : Input.GetAxis("Horizontal");
        _yInput = Input.GetAxis("Vertical");
        transform.Translate(_xInput * _speed * Time.deltaTime, 0, 0);
        _rigid2D.velocity = new Vector2(_speed * _xInput, _rigid2D.velocity.y);
        if (_xInput == 0)
        {
            // Is Idle
            _animator.SetBool("isIdle", true);
        }
        else
        {
            // Is Running
            _animator.SetBool("isIdle", false);

            if (!_audio.isPlaying)
            {
                _audio.PlayOneShot(_runSound);
            }
        }
    }

    void FlipPlayer()
    {
        if (_rigid2D.velocity.x < 0)
            _spriteRenderer.flipX = true;
        else if (_rigid2D.velocity.x > 0)
            _spriteRenderer.flipX = false;
      
    }

    public void jump()
    {
        _rigid2D.velocity = Vector2.up * _jumpForce;
    }

    /// <summary>
    /// Ham xu ly ve line ben duoi player.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _rayLength.y);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * _rayLength.x);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _rayLength.x);
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("door"))
        {
            Constants._currentTotalScore += Constants._curScoresInt;
            SceneManager.LoadScene(Constants.NextLevel);
        }
        
        if (collision.gameObject.CompareTag(Constants.NextLevel))
        {
            if (SceneManager.GetSceneByName(GameManager.Instance.GetNextLevel()) != null)
            {
                Constants._curLevelStr = GameManager.Instance.GetNextLevel();
                SceneManager.LoadScene(Constants._curLevelStr);
            }
            else
            {
                // End game.
            }
        }
        
        if (collision.gameObject.CompareTag("fire") || collision.gameObject.CompareTag("water") || collision.gameObject.CompareTag("sam_set"))
        {
            if (!_animator.GetBool("isDead"))
            {
                // Tru mang cua player.
                Vector2 vt = _rigid2D.velocity;
                vt.x = 0f;
                _rigid2D.velocity = vt;
                _animator.SetBool("isDead", true);
            }
        }
    }

    public void isDead()
    {
        // Set live hien tai.
        Constants._curLivesInt--;
        _canvasCtrl.SetLives(Constants._curLivesInt);

        // Check live.
        if (Constants._curLivesInt > 0)
        {
            Constants._curScoresInt = Constants._currentTotalScore;
            _canvasCtrl.SetScore(Constants._curScoresInt);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            //_camera.backgroundColor = Color.grey;
            _canvasCtrl.ShowReplayUI();
            Time.timeScale = 0f;    // Pause game.
        }

    }
}

using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private bool shouldMove { get; set; } = true;
    
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D _enemyRigidbody2D;
    private Vector2 _moveDirection;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _enemyRigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            _enemyRigidbody2D.MovePosition(_enemyRigidbody2D.position + _moveDirection * (moveSpeed * Time.deltaTime));
        }
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        _moveDirection = moveDirection;
        _spriteRenderer.flipX = _moveDirection.x < 0;
    }

    public void SetShouldMove(bool shouldMove)
    {
        if (!shouldMove)
        {
            _enemyRigidbody2D.velocity = Vector2.zero;
        }
        this.shouldMove = shouldMove;
    }

    public void StopMoving()
    {
        _moveDirection = Vector3.zero;
    }
}

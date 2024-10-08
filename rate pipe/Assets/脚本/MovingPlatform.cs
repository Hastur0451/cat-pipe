using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }
    public MoveDirection moveDirection = MoveDirection.Horizontal;
    public float moveDistance = 5f;
    public float speed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget = true;
    private Vector3 lastPosition;
    private Rigidbody2D playerRb;

    void Start()
    {
        startPosition = transform.position;
        lastPosition = startPosition;
        SetTargetPosition();
    }

    void FixedUpdate()
    {
        if (PauseManager.isPaused)
        {
            return; // 如果游戏暂停，则不移动平台
        }

        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingToTarget = !movingToTarget;
            SetTargetPosition();
        }

        // 计算平台的速度
        Vector3 velocity = (transform.position - lastPosition) / Time.fixedDeltaTime;

        // 更新玩家的速度
        if (playerRb != null)
        {
            playerRb.velocity += new Vector2(velocity.x, velocity.y);
        }

        // 更新 lastPosition
        lastPosition = transform.position;
    }

    void SetTargetPosition()
    {
        if (moveDirection == MoveDirection.Horizontal)
        {
            targetPosition = startPosition + (movingToTarget ? Vector3.right : Vector3.left) * moveDistance;
        }
        else
        {
            targetPosition = startPosition + (movingToTarget ? Vector3.up : Vector3.down) * moveDistance;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (moveDirection == MoveDirection.Horizontal)
        {
            Gizmos.DrawLine(startPosition, startPosition + Vector3.right * moveDistance);
            Gizmos.DrawLine(startPosition, startPosition + Vector3.left * moveDistance);
        }
        else
        {
            Gizmos.DrawLine(startPosition, startPosition + Vector3.up * moveDistance);
            Gizmos.DrawLine(startPosition, startPosition + Vector3.down * moveDistance);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRb = null;
        }
    }
}

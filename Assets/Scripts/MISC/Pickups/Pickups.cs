using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickups : MonoBehaviour
{
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float accelerationRate = .3f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY;
    [SerializeField] private float hangTime;
    [SerializeField] private float randomMinX;
    [SerializeField] private float randomMaxX;
    [SerializeField] private float randomMinY;
    [SerializeField] private float randomMaxY;
    [SerializeField] private PickupTypes pickupType;
    private Vector2 _moveDirection;
    private Rigidbody2D _pickupRigidbody;

    private enum PickupTypes
    {
        GoldCoin,
        HealthGlobe,
        StaminaGlobe
    }
    private void Awake()
    {
        _pickupRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimationCurveSpawnRoutine());
    }

    private IEnumerator AnimationCurveSpawnRoutine()
    {
        var startPosition = transform.position;
        Vector2 startPoint = startPosition;
        var endPoint = new Vector2(startPosition.x + Random.Range(randomMinX, randomMaxX),
            startPosition.y + Random.Range(randomMinY, randomMaxY));
        var timePassed = 0f;

        while (timePassed < hangTime)
        {
            timePassed += Time.deltaTime;
            var linearT = timePassed / hangTime;
            var heightT = animationCurve.Evaluate(timePassed);
            var height = Mathf.Lerp(0f, heightY, heightT);
            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }

    private void Update()
    {
        var playerPosition = PlayerController.Instance.transform.position;
        if (Vector3.Distance(transform.position, playerPosition) < pickupDistance)
        {
            _moveDirection = (playerPosition - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            _moveDirection = Vector2.zero;
            moveSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        _pickupRigidbody.velocity = _moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var playerController))
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private void DetectPickupType()
    {
        switch (pickupType)
        {
            case PickupTypes.GoldCoin:
            {
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            }
            case PickupTypes.HealthGlobe:
            {
                PlayerHealth.Instance.HealPlayer();
                break;
            }
            case PickupTypes.StaminaGlobe:
            {
                Stamina.Instance.RefreshStamina();
                break;
            }
        }
    }
}
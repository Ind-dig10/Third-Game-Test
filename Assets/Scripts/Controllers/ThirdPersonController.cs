using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{
	private float speed;
	private float acceleration;
	private float horizontal;
	private float vertical;

	public Transform rotate;
	private Rigidbody body;
	private Vector3 direction;


	private void Awake()
	{
		speed = DataManager.MovementSpeed;
		acceleration = DataManager.Acceleration;
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		gameObject.tag = DataManager.PLAYER_TAG;
	}

    private void FixedUpdate()
    {
		MovePlayer();
		LimitSpeedPlayer();
	}

    private void Update()
	{
		horizontal = Input.GetAxis(DataManager.HORIZONTAL);
		vertical = Input.GetAxis(DataManager.VERTICAL);

		RotationPlayer();
	}

	private void RotationPlayer()
    {
		direction = new Vector3(horizontal, 0, vertical);
		direction = Camera.main.transform.TransformDirection(direction);
		direction = new Vector3(direction.x, 0, direction.z);

		if (Mathf.Abs(vertical) > 0 || Mathf.Abs(horizontal) > 0) // разворот тела по вектору движения
		{
			rotate.rotation = Quaternion.Lerp(rotate.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
		}
	}

	private void LimitSpeedPlayer()
    {
		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * speed, body.velocity.y, body.velocity.z);
		}
		if (Mathf.Abs(body.velocity.z) > speed)
		{
			body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * speed);
		}
	}

	private void MovePlayer()
    {
		body.AddForce(direction.normalized * acceleration * body.mass * speed);
	}
}
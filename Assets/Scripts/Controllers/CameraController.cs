using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Чувствительность")]
	public float sensitivity = 2;

	[Header("Расстояние между камерой и игроком")]
	public float distance = 5;

	[Header("Высота камеры")]
	public float height = 2.3f;

	[Header("Смещение камеры")]
	public float offsetPosition;

	[Header("Угл при наклоне")]
	public float minY = 15f;
	public float maxY = 15f;

	public InversionX inversionX = InversionX.Disabled;
	public InversionY inversionY = InversionY.Disabled;

	public Smooth smooth = Smooth.Enabled;
	public float speed;

	private float rotationY;
	private int inversY, inversX;
	private Transform player;

	public enum InversionX { Disabled = 0, Enabled = 1 };
	public enum InversionY { Disabled = 0, Enabled = 1 };
	public enum Smooth { Disabled = 0, Enabled = 1 };

    private void Awake()
    {
		speed = DataManager.SpeedSmooth;
    }

    private void Start()
	{
		player = GameObject.FindGameObjectWithTag(DataManager.PLAYER_TAG).transform;
		gameObject.tag = DataManager.CAMERA_TAG;
	}

	private void LateUpdate()
	{
		if (!player)
			return;

		if (inversionX == InversionX.Disabled) inversX = 1; else inversX = -1;
		if (inversionY == InversionY.Disabled) inversY = -1; else inversY = 1;

		SpinninAroundPlayer();

		Vector3 position = DeterminePointPositionPlayer();

		CameraRotation();

		if (smooth == Smooth.Disabled) transform.position = position;
		else transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
	}

	private Vector3 CheckColliderPosition(Vector3 target, Vector3 position)
	{
		RaycastHit hit;

		if (Physics.Linecast(target, position, out hit))
		{
			float tempDistance = Vector3.Distance(target, hit.point);
			Vector3 pos = target - (transform.rotation * Vector3.forward * tempDistance);
			position = new Vector3(pos.x, position.y, pos.z); // сдвиг позиции в точку контакта
		}

		return position;
	}

	private void SpinninAroundPlayer()
	{
		transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X") * sensitivity * inversX);
	}

	private Vector3 DeterminePointPositionPlayer()
    {
		Vector3 position = player.position - (transform.rotation * Vector3.forward * distance);
		position = position + (transform.rotation * Vector3.right * offsetPosition);
		position = new Vector3(position.x, player.position.y + height, position.z);
		position = CheckColliderPosition(player.position, position);

		return position;
	}

	private void CameraRotation()
    {
		rotationY += Input.GetAxis(DataManager.MOUSE_Y) * sensitivity;
		rotationY = Mathf.Clamp(rotationY, -Mathf.Abs(minY), Mathf.Abs(maxY));
		transform.localEulerAngles = new Vector3(rotationY * inversY, transform.localEulerAngles.y, 0);
	}
}
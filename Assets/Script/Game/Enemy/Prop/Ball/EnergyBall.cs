using System.Collections;
using UnityEngine;

/// <summary>
/// 能量球
/// </summary>
public class EnergyBall : BaseBehaviour
{
	private float deltaTime => Time.time - startTime;
	
	private void OnEnable()
	{
		//生成
		startTime = Time.time;
		player = R.Player.Transform;
		transform.Find("Tail").gameObject.SetActive(true);
		transform.Find("Flash").gameObject.SetActive(true);
		//生成的随机位置  玩家和自己的位置的偏移值中判断位置
		Vector2 vector = new Vector2((player.GetComponent<Collider2D>().bounds.center - transform.position).x <= 0f ? Random.Range(0f, 1f) : Random.Range(-1f, 0f), Random.Range(-1f, 1f));
		randomDir = vector.normalized * 5f;//方向
		GetComponent<Rigidbody2D>().velocity = randomDir;//速度
		gameObject.GetComponent<Animator>().Play("EnergyBallFade");
		transform.Find("Tail").localPosition = Vector3.zero;
		Light component = light.GetComponent<Light>();
		component.intensity = 3f;//设置亮度
		StartCoroutine(UseBall());
	}

	private void Update()
	{
	}

	/// <summary>
	/// 使用能量球
	/// </summary>
	/// <returns></returns>
	private IEnumerator UseBall()
	{
		while (deltaTime <= waitTime)
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * 6f);
			if (Vector2.Angle(GetComponent<Rigidbody2D>().velocity, transform.position - player.position) > 160f && deltaTime <= waitTime)
			{
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0f));
		while (isMove)
		{
			transform.position = Vector3.MoveTowards(transform.position, player.position + repairPosition, 0.02f * speed * deltaTime);
			yield return new WaitForFixedUpdate();
			if (Vector3.Distance(transform.position, player.position + repairPosition) < 0.4f)
			{
				EnergyBallFunction(energyType);
				transform.Find("Tail").gameObject.SetActive(false);
				gameObject.GetComponent<Animator>().Play("EnergyBallCollision");
				GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
				transform.Find("Flash").gameObject.SetActive(false);
				//base.StartCoroutine(player.GetComponent<ChangeSpineColor>().EnergyBallColorChange());
				StartCoroutine(FadeInLight());
				break;
			}
		}
	}

	private void EnergyBallFunction(EnergyBallType energyType)
	{
		if (energyType != EnergyBallType.Armor)
		{
		}
	}

	private IEnumerator FadeInLight()
	{
		Light i = light.GetComponent<Light>();
		while (i.range < 6f)
		{
			i.range += 0.2f;
			yield return new WaitForSeconds(0.04f);
		}
		i.range = 4f;
		i.intensity = 0f;
	}

	[Header("是否可以移动")][SerializeField] private bool isMove = true;
	[Header("速度")][SerializeField] private float speed = 1f;
	[Header("最小距离")][SerializeField] private float minDistance = 0.1f;
	[Header("最大距离")]	[SerializeField] private float MaxDistance = 2.5f;
	[Header("能量球类型")][SerializeField] private EnergyBallType energyType;
	[Header("能量值")][SerializeField] private int energyValue = 10;
	[Header("等待时间")][SerializeField] private float waitTime;
	[Header("灯光")][SerializeField] private Transform light;

	private float startTime;

	private Transform player;

	private Vector3 repairPosition = new Vector3(0f, 1.2f, -0.1f);

	private Vector2 randomDir;

	/// <summary>
	/// 能量球类型
	/// </summary>
	public enum EnergyBallType
	{
		/// <summary>
		/// 装甲
		/// </summary>
		Armor = 1,
		
		/// <summary>
		/// 技能
		/// </summary>
		Skill
	}
}

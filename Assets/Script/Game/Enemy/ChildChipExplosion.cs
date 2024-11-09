using UnityEngine;

/// <summary>
/// 子芯片爆炸
/// </summary>
public class ChildChipExplosion : BaseBehaviour
{
    private float deltaTime => Time.time - startTime;

    private void OnEnable()
    {
        startTime = Time.time;
        backToPlayer = false;
        player = R.Player.Transform;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (backToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position + new Vector3(0f, 1.2f, 0f), 0.5f * playerChargingSpeed * deltaTime);
            if (Vector3.Distance(transform.position, player.position + new Vector3(0f, 1.2f, 0f)) < 0.1f)
            {
                R.Player.Action.AbsorbEnergyBall();
                backToPlayer = false;
                "子芯片爆炸切换颜色".Log();
                //base.StartCoroutine(player.GetComponent<ChangeSpineColor>().EnergyBallColorChange());
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public bool backToPlayer;

    public Transform player;

    private float startTime;

    private float playerChargingSpeed = 20f;
}
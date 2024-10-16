using System;
using UnityEngine;

/// <summary>
/// 存储点
/// </summary>
public class SavePoint : BaseBehaviour
{
    private bool _hasRecovered
    {
        get => RoundStorage.Get(LevelManager.SceneName + "HasRecovered", false);
        set => RoundStorage.Set(LevelManager.SceneName + "HasRecovered", value);
    }

    private bool HasConquer
    {
        get => SaveStorage.Get(LevelManager.SceneName + "HasConquer", false);
        set => SaveStorage.Set(LevelManager.SceneName + "HasConquer", value);
    }

    private void Start()
    {
        HasConquer = true;
        //R.Ui.Enhancement.OnFinish += this.OnEnhancementFinish;
    }

    private void OnDestroy()
    {
        if (!UIController.ApplicationIsQuitting)
        {
            //R.Ui.Enhancement.OnFinish -= this.OnEnhancementFinish;
            //R.Ui.SaveProgressCircle.Disappear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(CTag.Player) || _hasSaved) return;
        _hasSaved = true;
        if (!_hasRecovered)
        {
            R.Player.Attribute.AllAttributeRecovery();
            _hasRecovered = true;
        }

        R.GameData.Save();
        //测试成就
        AchievementManager.I.AwardAchievement(1);
        //_animator.SetTrigger("Save");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag(CTag.Player)) return;
        if (Input.Game.Search.OnClick)
        {
            //_prompt.FadeOut();
            //_animator.SetBool("IsShopping", true);
        }
    }

    private void OnEnhancementFinish(object sender, EventArgs e)
    {
        _animator.SetBool("IsShopping", false);
    }

    /// <summary>
    /// 是否已经保存
    /// </summary>
    private bool _hasSaved;

    [SerializeField] private Animator _animator;

    //[SerializeField]
    //private GateEnterPrompt _prompt;
}
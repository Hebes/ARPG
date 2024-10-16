using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

/// <summary>
/// 战斗评估管理
/// </summary>
public class BattleAssessmentManager : SMono<BattleAssessmentManager>
{
    private static float DeltaTime => Time.time - _lastComboTime;
    [SerializeField] private TextAsset _text;
    private JsonData _bloodData;
    private static float _lastComboTime;
    private bool _battleStart;
    private float _battleStartTime;
    private int _battleWave;
    private readonly List<float> _battleScore = new List<float>();
    private readonly List<float> _timeScore = new List<float>();
    private readonly List<string> _currentCombo = new List<string>();
    private readonly List<List<string>> _combo = new List<List<string>>();
    private PlayerAnimEvent _listener;

    private void OnEnable()
    {
        //_bloodData = JsonMapper.ToObject(_text.text);
        GameEvent.PlayerHurt.Register(PlayerHurtEventReceiver);
        // EventManager.RegisterEvent("EnemyHurtAtk", new EventManager.FBEventHandler<EnemyHurtAtkEventArgs>(EnemyHurtEventReceiver));
        // EventManager.RegisterEvent("Assessment", new EventManager.FBEventHandler<AssessmentEventArgs>(AssessmentEventReceiver));
    }

    private void OnDisable()
    {
        // EventManager.UnregisterEvent("PlayerHurt", new EventManager.FBEventHandler<EventArgs>(PlayerHurtEventReceiver));
        // EventManager.UnregisterEvent("EnemyHurtAtk", new EventManager.FBEventHandler<EnemyHurtAtkEventArgs>(EnemyHurtEventReceiver));
        // EventManager.UnregisterEvent("Assessment", new EventManager.FBEventHandler<AssessmentEventArgs>(AssessmentEventReceiver));
    }

    private void Update()
    {
        // if (DeltaTime >= AllowComboTime)
        // {
        //     ClearCurrentComboNum();
        // }
        // else
        // {
        //     R.Ui.HitsGrade.Hitbar = (AllowComboTime - DeltaTime) / AllowComboTime;
        // }
    }

    private float AllowComboTime
    {
        get
        {
            // int currentComboNum = R.SceneData.assessmentData.CurrentComboNum;
            // if (currentComboNum >= 30)
            // {
            //     return 2f;
            // }
            //
            // if (currentComboNum >= 20)
            // {
            //     return 3f;
            // }
            //
            // return (currentComboNum < 10) ? 5 : 4;
            return 1;
        }
    }

    public static void Reset()
    {
        _lastComboTime = 0f;
    }

    /// <summary>
    /// 添加当前组合数
    /// </summary>
    private void AddCurrentComboNum()
    {
        "添加当前组合数".Log();
        // _lastComboTime = Time.time;
        // R.Ui.HitsGrade.BeHited();
        // R.SceneData.assessmentData.CurrentComboNum++;
    }

    /// <summary>
    /// 清除当前连击数
    /// </summary>
    private void ClearCurrentComboNum()
    {
        "清除当前连击数".Log();
        // _lastComboTime = AllowComboTime;
        // if (R.SceneData.assessmentData.CurrentComboNum == 0) return;
        // if (R.SceneData.BloodPalaceMode)
        //     CalculateBattleScore();
        // R.SceneData.assessmentData.CurrentComboNum = 0;
        // R.Ui?.HitsGrade.HideHitNumAndBar();
    }

    /// <summary>
    /// 玩家受伤事件接收器
    /// </summary>
    /// <param name="udata"></param>
    private void PlayerHurtEventReceiver(object udata)
    {
        "玩家受伤事件接收器".Log();
        // var args = ((string, object, EventArgs))udata;
        // ClearCurrentComboNum();
        // if (!R.SceneData.BloodPalaceMode)
        // {
        //     return;
        // }
        //
        // R.SceneData.assessmentData.NotHurt = false;
        // StartBattleWave();
        // return;
    }

    private bool EnemyHurtEventReceiver(string eventName, object sender, EnemyHurtAtkEventArgs msg)
    {
        AddCurrentComboNum();
        if (R.SceneData.BloodPalaceMode)
        {
            BloodPalaceEnemyHurt(msg);
        }

        return true;
    }

    private bool AssessmentEventReceiver(string eventName, object sender, AssessmentEventArgs msg)
    {
        if (msg.Type == AssessmentEventArgs.EventType.ContinueGame)
        {
            ClearCurrentComboNum();
        }

        if (R.SceneData.BloodPalaceMode)
        {
            BloodPalaceAssessment(msg.Type);
        }

        return true;
    }

    private bool BattleRushEventReceiver(string eventdefine, object sender, BattleRushEventArgs msg)
    {
        "战斗冲刺事件".Log();
        // _battleWave = msg.Wave;
        // if (_battleWave == 0)
        // {
        //     return true;
        // }
        //
        // R.SceneData.LevelScore.WaveScore += _battleWave * 300;
        // FinishBattleWave(_battleWave - 1);
         return true;
    }

    private bool BattleEventReceiver(string eventdefine, object sender, BattleEventArgs msg)
    {
        "战斗事件".Log();
        // if (msg.Status == BattleEventArgs.BattleStatus.Begin)
        // {
        //     _battleScore.Clear();
        //     _timeScore.Clear();
        //     _currentCombo.Clear();
        //     _combo.Clear();
        //     _battleWave = 0;
        //     R.SceneData.assessmentData.NotHurt = true;
        //     R.SceneData.LevelScore.Clear();
        // }
        //
        // if (msg.Status == BattleEventArgs.BattleStatus.End)
        // {
        //     R.SceneData.LevelScore.WaveScore += _battleWave * 300;
        //     FinishBattleWave(_battleWave);
        //     ClearCurrentComboNum();
        //     CalculateFinalScore();
        // }

        return true;
    }

    /// <summary>
    /// 保存总分
    /// </summary>
    /// <returns></returns>
    // public Coroutine SaveTotalScore()
    // {
    //     "保存总分".Log();
    //     // List<BloodPalaceTotalScore> records = R.GameData.BloodPalaceRecords;
    //     // BloodPalaceTotalScore totalScore = R.SceneData.TotalScore;
    //     // if (!records.Contains(R.SceneData.TotalScore))
    //     // {
    //     //     records.Add(totalScore);
    //     // }
    //     //
    //     // records.Sort();
    //     // if (records.Count > 10)
    //     // {
    //     //     records.RemoveAt(10);
    //     // }
    //     //
    //     // return SaveManager.ModifySaveData(delegate(GameData gameData)
    //     // {
    //     //     gameData.BloodPalaceRecords.Clear();
    //     //     for (int i = 0; i < records.Count; i++)
    //     //     {
    //     //         gameData.BloodPalaceRecords.Add(records[i]);
    //     //     }
    //     // });
    //     
    // }

    public void EnterBloodPalace()
    {
        R.SceneData.BloodPalaceMode = true;
        // EventManager.RegisterEvent("Battle", new EventManager.FBEventHandler<BattleEventArgs>(BattleEventReceiver));
        // EventManager.RegisterEvent("BattleRush", new EventManager.FBEventHandler<BattleRushEventArgs>(BattleRushEventReceiver));
        _listener = R.Player.GetComponent<PlayerAnimEvent>();
        GameEvent.OnPlayerDead.Register(OnPlayerDead);
        R.SceneData.TotalScore = new BloodPalaceTotalScore();
    }

    public void ExitBloodPalace()
    {
        // EventManager.UnregisterEvent("Battle", new EventManager.FBEventHandler<BattleEventArgs>(BattleEventReceiver));
        // EventManager.UnregisterEvent("BattleRush", new EventManager.FBEventHandler<BattleRushEventArgs>(BattleRushEventReceiver));
        // _listener.OnPlayerDead -= OnPlayerDead;
        // R.SceneData.BloodPalaceMode = false;
    }

    private void BloodPalaceEnemyHurt(EnemyHurtAtkEventArgs msg)
    {
        // StartBattleWave();
        // switch (msg.hurtType)
        // {
        //     case EnemyHurtAtkEventArgs.HurtTypeEnum.Normal:
        //         AddAirComboNum();
        //         AddToCurrentCombo(msg.attackData.atkName);
        //         R.SceneData.assessmentData.ComboData.AllDamagePercent += msg.attackData.damagePercent;
        //         break;
        //     case EnemyHurtAtkEventArgs.HurtTypeEnum.Execute:
        //     case EnemyHurtAtkEventArgs.HurtTypeEnum.QTEHurt:
        //         R.SceneData.assessmentData.ComboData.CoreBreakNum++;
        //         break;
        //     case EnemyHurtAtkEventArgs.HurtTypeEnum.Flash:
        //         R.SceneData.assessmentData.FlashAttackSuccessNum++;
        //         R.SceneData.assessmentData.ComboData.FlashAttackSuccessNum++;
        //         break;
        // }
    }

    private void BloodPalaceAssessment(AssessmentEventArgs.EventType type)
    {
        // if (type != AssessmentEventArgs.EventType.AddFlashTime)
        // {
        //     if (type == AssessmentEventArgs.EventType.CurrentComboFinish)
        //     {
        //         AddToCombo();
        //     }
        // }
        // else
        // {
        //     R.SceneData.assessmentData.FlashAttackNum++;
        // }
    }

    private void OnPlayerDead(object sender)
    {
        EventArgs e = (EventArgs)sender;
        FinishBattleWave(_battleWave);
        ClearCurrentComboNum();
        CalculateFinalScore();
    }

    private void StartBattleWave()
    {
        if (!_battleStart)
        {
            _battleStart = true;
            _battleStartTime = Time.time;
        }
    }

    private void FinishBattleWave(int waveNum)
    {
        _battleStart = false;
        _timeScore.Add(CalculateTimeScore(Time.time - _battleStartTime, waveNum));
    }

    /// <summary>
    /// 计算战斗分数
    /// </summary>
    private void CalculateBattleScore()
    {
        // AddToCombo();
        // CalculateSameCombo();
        // BattleAssessmentData.BattleComboData comboData = R.SceneData.assessmentData.ComboData;
        // float item = (R.SceneData.assessmentData.CurrentComboNum - comboData.SameComboNum + comboData.AirComboNum * 0.5f +
        //               comboData.FlashAttackSuccessNum * 5 + comboData.CoreBreakNum * 2) *
        //              (comboData.AllDamagePercent / R.SceneData.assessmentData.CurrentComboNum) * 20f;
        // _battleScore.Add(item);
        // R.SceneData.assessmentData.ComboClear();
    }

    private void CalculateFinalScore()
    {
        // BloodPalaceLevelScore levelScore = R.SceneData.LevelScore;
        // levelScore.BeautyScore = (int)AverageBattleScore();
        // levelScore.NotHurt = R.SceneData.assessmentData.NotHurt;
        // float num = (levelScore.WaveScore + AverageBattleScore()) * AverageBattleTime() * FlashAttackScore();
        // levelScore.OriginalScore = (int)num;
        // R.SceneData.TotalScore.AddLevelScore(levelScore);
        // SaveTotalScore();
        // _battleScore.Clear();
        // _timeScore.Clear();
        // R.SceneData.assessmentData.BattleClear();
    }

    private void AddToCombo()
    {
        if (_currentCombo.Count <= 0)
        {
            return;
        }

        _combo.Add(_currentCombo);
        _currentCombo.Clear();
    }

    private void AddToCurrentCombo(string comboName)
    {
        if (comboName == "AirAtk7")
        {
            return;
        }

        if (_currentCombo.Count <= 0 || _currentCombo[_currentCombo.Count - 1] != comboName)
        {
            _currentCombo.Add(comboName);
        }
    }

    private void AddAirComboNum()
    {
        // if (!R.Player.Attribute.isOnGround)
        // {
        //     R.SceneData.assessmentData.ComboData.AirComboNum++;
        // }
    }

    private void CalculateSameCombo()
    {
        // BattleAssessmentData.BattleComboData comboData = R.SceneData.assessmentData.ComboData;
        // if (_combo.Count <= 1)
        // {
        //     comboData.SameComboNum = 0;
        //     _combo.Clear();
        //     return;
        // }
        //
        // for (int i = 1; i < _combo.Count; i++)
        // {
        //     List<string> oldCombo = _combo[i - 1];
        //     List<string> newCombo = _combo[i];
        //     AddSameComboNum(oldCombo, newCombo);
        // }
        //
        // _combo.Clear();
    }

    private void AddSameComboNum(List<string> oldCombo, List<string> newCombo)
    {
        // for (int i = 0; i < newCombo.Count; i++)
        // {
        //     if (oldCombo.Contains(newCombo[i]))
        //     {
        //         R.SceneData.assessmentData.ComboData.SameComboNum++;
        //     }
        // }
    }

    private float CalculateTimeScore(float time, int waveNum)
    {
        // R.SceneData.LevelScore.Time += time;
        // float num = _bloodData[LevelManager.SceneName].Get(waveNum, 0f);
        // if (time <= num)
        // {
        //     return 2.5f;
        // }
        //
        // if (time <= num * 1.2f)
        // {
        //     return 2f;
        // }
        //
        // if (time <= num * 1.6f)
        // {
        //     return 1.5f;
        // }
        //
        // return (time > num * 1.8f) ? 1f : 1.2f;
        return 1;
    }

    private float AverageBattleTime()
    {
        float num = 0f;
        for (int i = 0; i < _timeScore.Count; i++)
        {
            num += _timeScore[i];
        }

        return num / _timeScore.Count;
    }

    private float AverageBattleScore()
    {
        float num = 0f;
        if (_battleScore.Count == 0)
        {
            return 0f;
        }

        for (int i = 0; i < _battleScore.Count; i++)
        {
            num += _battleScore[i];
        }

        return num / _battleScore.Count;
    }

    private float FlashAttackScore()
    {
        // BattleAssessmentData assessmentData = R.SceneData.assessmentData;
        // BloodPalaceLevelScore levelScore = R.SceneData.LevelScore;
        // if (assessmentData.FlashAttackNum <= 0)
        // {
        //     levelScore.FlashPercent = 0f;
        //     return 0.7f;
        // }
        //
        // float num = assessmentData.FlashAttackSuccessNum * 1f / assessmentData.FlashAttackNum;
        // levelScore.FlashPercent = num;
        // if (num >= 0.95f)
        // {
        //     return 2f;
        // }
        //
        // if (num >= 0.8f)
        // {
        //     return 1.8f;
        // }
        //
        // if (num >= 0.7f)
        // {
        //     return 1.5f;
        // }
        //
        // if (num >= 0.5f)
        // {
        //     return 1.3f;
        // }
        //
        // return (num < 0.2f) ? 0.7f : 1f;
        return 1;
    }

    
}
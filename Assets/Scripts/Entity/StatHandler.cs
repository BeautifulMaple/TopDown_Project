using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // 캐릭터의 스탯 데이터를 저장하는 ScriptableObject
    public StatData statData;

    // 현재 캐릭터의 스탯을 저장하는 딕셔너리
    private Dictionary<StatType, float> currentStats = new Dictionary<StatType, float>();

    private void Awake()
    {
        // 게임 시작 시 스탯을 초기화
        InitializeStats();
    }

    /// <summary>
    /// 캐릭터의 스탯을 초기화하는 메서드
    /// </summary>
    private void InitializeStats()
    {
        // statData에서 기본 스탯 값을 가져와 currentStats에 저장
        foreach (StatEntry entry in statData.stats)
        {
            currentStats[entry.statType] = entry.baseValue;
        }
    }

    /// <summary>
    /// 특정 스탯의 현재 값을 반환하는 메서드
    /// </summary>
    /// <param name="statType">조회할 스탯의 타입</param>
    /// <returns>해당 스탯의 현재 값 (없다면 0 반환)</returns>
    public float GetStat(StatType statType)
    {
        return currentStats.ContainsKey(statType) ? currentStats[statType] : 0;
    }

    /// <summary>
    /// 스탯 값을 변경하는 메서드
    /// </summary>
    /// <param name="statType">변경할 스탯 타입</param>
    /// <param name="amount">증가 또는 감소할 값</param>
    /// <param name="isPermanent">변경이 영구적인지 여부 (기본값: true)</param>
    /// <param name="duration">변경이 지속될 시간 (기본값: 0, 영구 적용됨)</param>
    public void ModifyStat(StatType statType, float amount, bool isPermanent = true, float duration = 0)
    {
        // 해당 스탯이 존재하지 않으면 처리하지 않음
        if (!currentStats.ContainsKey(statType)) return;

        // 스탯 값 변경
        currentStats[statType] += amount;

        // 일시적인 변경이면 일정 시간 후 원래 값으로 복구
        if (isPermanent)
        {
            StartCoroutine(RemoveStatAfterDuration(statType, amount, duration));
        }
    }

    /// <summary>
    /// 일정 시간이 지난 후 일시적으로 증가한 스탯을 원래 값으로 복구하는 코루틴
    /// </summary>
    /// <param name="statType">복구할 스탯 타입</param>
    /// <param name="amount">복구할 값</param>
    /// <param name="duration">지속 시간</param>
    private IEnumerator RemoveStatAfterDuration(StatType statType, float amount, float duration)
    {
        // 설정된 시간만큼 대기
        yield return new WaitForSeconds(duration);

        // 스탯 값을 원래대로 되돌림
        currentStats[statType] -= amount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Algorithm
{
    /// <summary>
    /// 내비메시 위의 랜덤한 위치를 반환하는 메서드.
    /// center를 중심으로 distance 반경 안에서 랜덤한 위치를 찾는다.
    /// 랜덤한 한 점을 찾기에 실패하면 양의 무한 벡터 반환.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center를 중심으로 반지름이 maxDistance인 구 안에서의 랜덤한 위치 하나를 저장
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = center + Random.insideUnitSphere * distance;

        // 내비메시 샘플링의 결과 정보를 저장하는 변수
        NavMeshHit hit;

        // distance 반경 안에서, randomPos에 가장 가까운 내비메시 위의 한 점을 찾음
        if (NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas))
            // 찾은 점 반환
            return hit.position;
        return Vector3.positiveInfinity;
    }

    /// <summary>
    /// 각도 구하는 함수,
    /// transform.forward 기준
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static float GetAngle(Transform start, Transform end)
    {
        return Mathf.Atan2(start.forward.z, end.forward.x) * Mathf.Rad2Deg;
    }
}

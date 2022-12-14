using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Algorithms
{
    /// <summary>
    /// ����޽� ���� ������ ��ġ�� ��ȯ�ϴ� �޼���.
    /// center�� �߽����� distance �ݰ� �ȿ��� ������ ��ġ�� ã�´�.
    /// ������ �� ���� ã�⿡ �����ϸ� ���� ���� ���� ��ȯ.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center�� �߽����� �������� maxDistance�� �� �ȿ����� ������ ��ġ �ϳ��� ����
        // Random.insideUnitSphere�� �������� 1�� �� �ȿ����� ������ �� ���� ��ȯ�ϴ� ������Ƽ
        Vector3 randomPos = center + Random.insideUnitSphere * distance;

        // ����޽� ���ø��� ��� ������ �����ϴ� ����
        NavMeshHit hit;

        // distance �ݰ� �ȿ���, randomPos�� ���� ����� ����޽� ���� �� ���� ã��
        if (NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas))
            // ã�� �� ��ȯ
            return hit.position;
        return Vector3.positiveInfinity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3D : MonoBehaviour
{
    /// <summary>
    /// transform.forward 기준으로 원하는 각도까지 회전하는 함수
    /// </summary>
    /// <param name="targetPosition"></param>: 회전할 타겟 위치 
    /// <param name="rotateTime"></param>: 회전 시간
    /// <returns></returns>
    public IEnumerator Rotate(Vector3 targetPosition)
    {
        var myPosition = transform.position;
        myPosition.y = 0f;
        var myForward = transform.forward;
        myForward.y = 0f;
        var targetDir = targetPosition - myPosition;
        targetDir.y = 0f;
        var signedAngle = Vector3.SignedAngle(myForward, targetDir, Vector3.up);
        var curDeltaTime = Time.deltaTime;
        var totalFrame = Mathf.RoundToInt(rotateTime / curDeltaTime);
        var curAngle = transform.eulerAngles;
        var anglePerFrame = signedAngle / totalFrame;

        for (var i = 1; i <= totalFrame; i++)
        {
            transform.eulerAngles = new Vector3(
                curAngle.x,
                curAngle.y + anglePerFrame * i,
                curAngle.z
            );
            yield return null;
        }
    }

    public float rotateTime = 1f;
}

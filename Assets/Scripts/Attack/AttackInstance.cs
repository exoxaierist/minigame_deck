using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInstance : MonoBehaviour
{
    public bool active = false;
    private AttackInfo payload;
    private SpriteRenderer spr;

    // 공격하기
    public void Attack(Vector2 _pos, AttackInfo _payload)
    {
        gameObject.SetActive(true);
        active = true;
        transform.SetParent(null);
        transform.position = _pos;
        payload = _payload;
        // 콜리젼마스크 설정 todo
        Collider2D hit = Physics2D.OverlapPoint(_pos,Global.collisionPlayer);
        if(hit!=null && hit.TryGetComponent(out IReceiveAttack dest))
        {
            dest.ReceivePayload(payload);
        }
        StartCoroutine(ShowEffect());
    }

    private IEnumerator ShowEffect()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        spr.enabled = false;
        Remove();
    }

    // 이 오브젝트 지움
    private void Remove()
    {
        active = false;
        Global.atkPooler.Deactivate(this);
    }
}

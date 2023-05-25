using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreprocessingUnit : UnitBase, IReceiveAttack
{
    protected Vector2 prevPos;
    protected Vector2 lookDirection;
    protected override void Awake()
    {
        base.Awake();
        prevPos = transform.position * Vector2.one;
    }

    // 피격시 해당 함수 호출
    public virtual void ReceivePayload(AttackInfo _info)
    {
        AttackInfo preprocessedAttackInfo = PreprocessingDamage(_info);
        hp.AddToHP(preprocessedAttackInfo.damage); // 추후 데미지 받는 방식의 수정에 따라 AttackInfo로 변경될 수 있음
    }

    //데미지를 전처리하는 함수
    public virtual AttackInfo PreprocessingDamage(AttackInfo _info)
    {
        AttackInfo preprocessedAttackInfo = _info;
        //데미지 감소시키는 코드
        //이후 체력에게 데이터를 넣어줌
        return preprocessedAttackInfo;
    }
    
    //이전 위치 갱신 및 보고 있는 방향을 받는 함수
    protected virtual void getLookDirection()
    {
        //움직일때 작동하는 함수이므로 움직이지 않은 경우는 배제함
        //한번 움직일때 x,y 둘 다 바뀌는, 즉 대각선 이동이 없다고 가정
        lookDirection = Vector2.zero;
        Vector2 movePos = prevPos - transform.position * Vector2.one;
        if(movePos.x == 0)
        {
            lookDirection = movePos.y < 0 ? new Vector2(1, -1) : new Vector2(1, 1);
        }
        else
        {
            lookDirection = movePos.x < 0 ? new Vector2(-1, 1) : new Vector2(1, 1);
        }
    }

    //공격하는 함수
    protected virtual void Attack(AttackInfo _attackInfo)
    {
        // 보고 있는 방향으로 공격 이펙트 생성 및 이 유닛의 AttackInfo 전달
        // AttackInfo에 공격 좌표쪽도 넣으면 좋을듯?
        // AttakEffectInstansiate(AttackInfo.attackPos[index] * lookDirction)
    }
    protected override void OnMove()
    {
        getLookDirection();
    }
}

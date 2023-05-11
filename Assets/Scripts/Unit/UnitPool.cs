using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPool : MonoBehaviour
{
    //유닛 생성을 어떻게 할지
    //클릭으로 소환한다면 격자를 화면에 그릴지
    //소환 함수를 이쪽에 코드를 만들지
    //인접의 기준이 인접만 혹은 3X3
    public static UnitPool Instance;
    [SerializeField]
    public List<UnitBase> P1Unit;
    [SerializeField]
    public List<UnitBase> P2Unit;
    private void singletoneCheck()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Awake()
    {
        singletoneCheck();
    }
}

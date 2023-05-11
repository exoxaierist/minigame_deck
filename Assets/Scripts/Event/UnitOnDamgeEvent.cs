using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class UnitOnDamgeEvent : MonoBehaviour 
{   
    private :
        int damge;
        BaseUnit attacker;
        BaseUnit defender;
    UnityEvent event ; 
    public:
        UnitOnDamgeEvent ( int a , BaseUnit attack , BaseUnit defend) {
            damge = a;
            attacker = attack
            defender = defend
        }
        void Start() { 
            if (event == null) {
                event = new UnityEvent();
        }
        }
        void getDamge() {
            return damge;
        }
        void getAttacker (){
            return attacker;
        }
        void getDefender () { 
            return defender;
        }
}

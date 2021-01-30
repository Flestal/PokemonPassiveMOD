using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579016:PassiveAbilityBase
    {
        public override bool isInvincible
        {
            get
            {
                return true;
            }
        }
        public override string debugDesc
        {
            get
            {
                return "통상 공격에 대해 무적, 자신의 방어 속성 중 가장 취약한 속성으로 피격 시 최대 체력의 50% 감소";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            MostWeak = BehaviourDetail.Slash;
            if (this.owner.Book.GetResistHP(MostWeak) > this.owner.Book.GetResistHP(BehaviourDetail.Penetrate))
            {
                MostWeak = BehaviourDetail.Penetrate;
            }
            if (this.owner.Book.GetResistHP(MostWeak) > this.owner.Book.GetResistHP(BehaviourDetail.Hit))
            {
                MostWeak = BehaviourDetail.Hit;
            }
        }
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (atkDice.Detail == MostWeak)
            {
                //this.owner.hp -= Convert.ToInt32(this.owner.MaxHp * 0.5);
                this.owner.TakeDamage(Convert.ToInt32(this.owner.MaxHp * 0.5));
                if (this.owner.hp <= (float)this.owner.Book.DeadLine)
                {
                    if (this.owner.lastAttacker != null)
                    {
                        this.owner.Die(this.owner.lastAttacker);
                    }
                    else
                    {
                        this.owner.Die(null);
                    }
                }
            }
        }
        BehaviourDetail MostWeak;
    }
}

using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579024:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "공격 주사위 위력 +5, 매 공격 시 20% 확률로 주사위 값이 1이 됨";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            flag = RandomUtil.valueForProb < 0.2f;
            if (this.IsAttackDice(behavior.Detail)&&!flag)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 5
                });
            }
            else if(this.isAtk(behavior.Detail))
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = -9999
                });
            }
        }
        /*public override void ChangeDiceResult(BattleDiceBehavior behavior, ref int diceResult)
        {
            if (flag)
            {
                if (this.owner.battleCardResultLog!=null)
                {
                    this.owner.battleCardResultLog.SetPassiveAbility(this);
                }
                diceResult = 1;
            }
        }*/
        protected bool isAtk(BehaviourDetail diceDetail)
        {
            return diceDetail == BehaviourDetail.Slash || diceDetail == BehaviourDetail.Penetrate || diceDetail == BehaviourDetail.Hit;
        }
        private bool flag;
    }
}

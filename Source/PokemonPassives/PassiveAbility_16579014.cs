using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579014:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "매 막마다 처음 적중한 주사위 종류의 피해량 x1.5";
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
        public override void OnRoundStart()
        {
            detail = BehaviourDetail.None;
        }
        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            if (detail == BehaviourDetail.None)
            {
                detail = behavior.Detail;
            }
            if (behavior.Detail == detail)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    dmgRate = 50
                });
            }
        }
        private BehaviourDetail detail;
    }
}

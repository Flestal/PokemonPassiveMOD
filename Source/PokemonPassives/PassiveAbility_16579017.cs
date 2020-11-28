using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579017:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "피해를 받을 시 상대에게 피해량만큼 화상 부여";
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
        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            atkDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, dmg);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579006:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "피해를 받을 시 자신에게 신속 1, 힘 1, 취약 1 부여";
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
            base.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 1, base.owner);
            base.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, base.owner);
            base.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, base.owner);
        }
    }
}

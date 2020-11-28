using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579010:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "매 막마다 체력이 100%일 경우 그 막 동안 자신이 받는 피해 x0.5";
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
            multiscale = (base.owner.hp == base.owner.MaxHp);
        }
        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (multiscale)
            {
                //dmg = Convert.ToInt32(dmg * 0.5f);
                this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, Convert.ToInt32(dmg * 0.5f));
                multiscale = false;
                return true;
            }
            return false;
        }
        private bool multiscale;
    }
}

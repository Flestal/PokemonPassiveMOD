using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579023:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "자신 사망 시 모두에게 최대 체력의 20% 피해";
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
        public override void OnDie()
        {
            List<BattleUnitModel> alivelist = BattleObjectManager.instance.GetAliveList();
            foreach (BattleUnitModel target in alivelist)
            {
                if (target == base.owner) continue;
                target.TakeDamage(Convert.ToInt32(target.MaxHp * 0.2f));
            }
        }
    }
}

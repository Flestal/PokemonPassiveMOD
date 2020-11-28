using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579026:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대 시작 시 자신의 최대 체력의 33%만큼 회복";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            this.owner.RecoverHP(Convert.ToInt32(this.owner.MaxHp*0.33f));
        }
    }
}

using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579021:PassiveAbilityBase
    {
        public override bool isInvincible
        {
            get
            {
                return invincible;
            }
        }
        public override string debugDesc
        {
            get
            {
                return "자신의 체력이 100%일 경우 죽음에 이르는 피해를 받을 시 체력을 1 남기고 그 막 동안 무적";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            invincible = false;
        }
        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (this.owner.hp == this.owner.MaxHp && this.owner.hp < dmg)
            {
                this.owner.hp = 1;
                invincible = true;
            }
            return true;
        }
        public override void OnRoundEnd()
        {
            invincible = false;
        }
        private bool invincible;
    }
}

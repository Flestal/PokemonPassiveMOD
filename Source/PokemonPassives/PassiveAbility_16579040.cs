using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579040:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "막 시작 시 화상에 의한 피해를 절반으로 받음";
            }
        }
        public override bool IsImmune(KeywordBuf buf)
        {
			return buf == KeywordBuf.Burn || buf == KeywordBuf.BurnBreak;
        }
        public override void OnRoundStart()
        {
			int stack_burn=0;
			int stack_burnBreak=0;
			if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.Burn))
			{
				stack_burn = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Burn).stack;
				
				if (this.owner.bufListDetail.GetReadyBufList().Exists((BattleUnitBuf x) => x.bufType == KeywordBuf.BurnBreak))
				{
					stack_burnBreak = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Burn).stack;
				}
				if (stack_burn > 0)
				{
					this.owner.TakeDamage(stack_burn / 2,DamageType.Passive, null);
				}
				if (stack_burnBreak > 0)
				{
					this.owner.TakeBreakDamage(stack_burnBreak / 4,DamageType.Passive, null, AtkResist.Normal);
				}
				SingletonBehavior<DiceEffectManager>.Instance.CreateBufEffect("BufEffect_Burn", this.owner.view);
            }
			
		}
    }
}

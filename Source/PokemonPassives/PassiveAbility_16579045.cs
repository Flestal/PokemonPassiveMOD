﻿using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579045:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "행동 불가 상태에 걸리지 않음";
            }
        }
        public override bool IsImmune(KeywordBuf buf)
        {
            return buf == KeywordBuf.Stun;
        }
    }
}

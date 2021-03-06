﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model;
using SkfrgSimCommon.Model.Abilities.Paladin;

namespace SkfrgSimCommon.Classes
{
	public class Paladin : Actor
	{
		public Paladin(EnvironmentContext context, ActorStats stats)
			: base(context, stats)
		{
			Abilities = new Dictionary<string, Ability>() { 
				{ AbilityNames.Paladin.LKM, new RighteousBlow() },
				{ AbilityNames.Paladin.LKMx2, new PunisherBlow() },
				{ AbilityNames.Paladin.LKMx3, new JudgeBlow() },
				{ AbilityNames.Paladin.LKMx4, new SwordOfJustice() },
				{ AbilityNames.Paladin.LKMx2PKM, new PunishingLightning() },
				{ AbilityNames.Paladin.HolyGround, new HolyGround() },
				{ AbilityNames.Paladin.Ability4, new FuriousJudgement() }
			};

			MaxResource = 400;
			CurrentResource = MaxResource;
			ResourceRechargeRate = 1000;
			ResourceRechargeValue = 5;
		}

        string prevComboAbility = null;
		protected override string SelectAbility(EnvironmentContext context)
		{
			if (!Abilities[AbilityNames.Paladin.Ability4].IsOnCd(context.CurrentTime) && prevComboAbility == null)
			{
				return AbilityNames.Paladin.Ability4;
			}

            if (prevComboAbility == null)
			{
                prevComboAbility = AbilityNames.Paladin.LKM;
				return AbilityNames.Paladin.LKM;
			}

			if (prevComboAbility == AbilityNames.Paladin.LKMx3)
			{
                prevComboAbility = null;
				return AbilityNames.Paladin.LKMx4;
			}

            if (CurrentResource < MaxResource)
            {
                if (!Abilities[AbilityNames.Paladin.HolyGround].IsOnCd(context.CurrentTime))
                {
                    return AbilityNames.Paladin.HolyGround;
                }
            }

            if (prevComboAbility == AbilityNames.Paladin.LKMx2)
			{
				var lightningCurrentParams = GetAbilityParams(AbilityNames.Paladin.LKMx2PKM);
                if (CurrentResource >= lightningCurrentParams.BaseParams.ResourceCost)
                {
                    prevComboAbility = null;
                    return AbilityNames.Paladin.LKMx2PKM;
                }
                else
                {
                    prevComboAbility = AbilityNames.Paladin.LKMx3;
                    return AbilityNames.Paladin.LKMx3;
                }
			}

            if (prevComboAbility == AbilityNames.Paladin.LKM)
            {
                prevComboAbility = AbilityNames.Paladin.LKMx2;
                return AbilityNames.Paladin.LKMx2;
            }

            prevComboAbility = AbilityNames.Paladin.LKM;
			return AbilityNames.Paladin.LKM;
		}
	}
}

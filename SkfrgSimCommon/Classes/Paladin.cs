using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model;

namespace SkfrgSimCommon.Classes
{
	public class Paladin : Actor
	{
		public Paladin(EnvironmentContext context, ActorStats stats, Calculator clc)
			: base(context, stats, clc)
		{
			Abilities = new Dictionary<string, Ability>() { 
				{ AbilityNames.Paladin.LKM, new Ability() { Parameters = new AbilityParams() { Name = AbilityNames.Paladin.LKM, CastTime = 400, CoolDown = 0, DmgCoeff = 0.06, ImpulseDmgCoeff = 0, IsUseImpulse = false, ResourceCost = 0 }}},
				{ AbilityNames.Paladin.LKMx2, new Ability() { Parameters = new AbilityParams() { Name = AbilityNames.Paladin.LKMx2, CastTime = 400, CoolDown = 0, DmgCoeff = 0.19, ImpulseDmgCoeff = 0, IsUseImpulse = false, ResourceCost = 0 }}},
				{ AbilityNames.Paladin.LKMx3, new Ability() { Parameters = new AbilityParams() { Name = AbilityNames.Paladin.LKMx3, CastTime = 600, CoolDown = 0, DmgCoeff = 0.46, ImpulseDmgCoeff = 0, IsUseImpulse = false, ResourceCost = 0 }}},
				{ AbilityNames.Paladin.LKMx4, new Ability() { Parameters = new AbilityParams() { Name = AbilityNames.Paladin.LKMx4, CastTime = 1600, CoolDown = 0, DmgCoeff = 0.68, ImpulseDmgCoeff = 0, IsUseImpulse = false, ResourceCost = -70 }}},
				{ AbilityNames.Paladin.LKMx2PKM, new Ability() { Parameters = new AbilityParams() { Name = AbilityNames.Paladin.LKMx2PKM, CastTime = 1200, CoolDown = 0, DmgCoeff = 0.68, ImpulseDmgCoeff = 1, IsUseImpulse = true, ResourceCost = 130 }}}
			};

			IsImpulseAvailable = true;
			MaxResource = 400;
			CurrentResource = MaxResource;
			ResourceRechargeRate = 1000;
			ResourceRechargeValue = 5;
		}

		protected override Ability SelectAbility(EnvironmentContext context)
		{
			if (previousUsedAbility == null)
			{
				return UseAbility(AbilityNames.Paladin.LKM);
			}

			if (previousUsedAbility != null && previousUsedAbility.Parameters.Name == AbilityNames.Paladin.LKMx3)
			{
				return UseAbility(AbilityNames.Paladin.LKMx4);
			}

			if (previousUsedAbility != null && previousUsedAbility.Parameters.Name == AbilityNames.Paladin.LKMx2)
			{
				var lightning = GetByName(AbilityNames.Paladin.LKMx2PKM);
				if (CurrentResource >= lightning.Parameters.ResourceCost)
					return UseAbility(AbilityNames.Paladin.LKMx2PKM);
				else
					return UseAbility(AbilityNames.Paladin.LKMx3);
			}

			if (previousUsedAbility != null && previousUsedAbility.Parameters.Name == AbilityNames.Paladin.LKM)
				return UseAbility(AbilityNames.Paladin.LKMx2);

			return UseAbility(AbilityNames.Paladin.LKM);
			//return UseAbility(AbilityNames.PaladinLKMx4);
		}
	}
}

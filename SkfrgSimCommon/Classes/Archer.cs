using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model;
using SkfrgSimCommon.Model.Abilities.Archer;

namespace SkfrgSimCommon.Classes
{
	public class Archer : Actor
	{
		public Archer(EnvironmentContext context, ActorStats stats)
			: base(context, stats)
		{
			Abilities = new Dictionary<string, Ability>() { 
				{ AbilityNames.Archer.AimedShot, new AimedShot() },
				{ AbilityNames.Archer.StandardShot, new StandardShot() },
				{ AbilityNames.Archer.FireArrow, new FireArrow() },
				{ AbilityNames.Archer.PiercingShot, new PiercingShot() },
				{ AbilityNames.Archer.LongAimedShot, new LongAimedShot() },
				{ AbilityNames.Archer.FireShelling, new FireShelling() },
			};

			IsImpulseAvailable = true;
			MaxResource = 300;
			CurrentResource = MaxResource;
			ResourceRechargeRate = 1000;
			ResourceRechargeValue = 5;
		}

		protected override string SelectAbility(EnvironmentContext context)
		{
			// начинаем с прицельного
			//if (previousUsedAbility == null)
			//    return AbilityNames.Archer.LongAimedShot;

			// если не висит горение - вешаем огненную стрелу
			var fireDot = context.Actor.Buffs.FirstOrDefault(b => b.Buff.Name == BuffNames.Archer.BurningDot);
			if (fireDot == null)
			{
				var p = context.Actor.GetAbilityParams(AbilityNames.Archer.FireArrow);

				if (context.Actor.CurrentResource >= p.BaseParams.ResourceCost)
					return AbilityNames.Archer.FireArrow;
			}

			// если висит бафф бесплатного обстрела и горение будет висеть достаточно долго используем обстрел
			//if (context.Actor.Buffs.FirstOrDefault(b => b.Buff.Name == BuffNames.Archer.FireShellingBuff) != null)
			//{ 
			//    var shelling = context.Actor.GetAbilityParams(AbilityNames.Archer.FireShelling);
			//    if (fireDot != null && fireDot.EndTime > context.CurrentTime + shelling.BaseParams.TotalCastTime)
			//    {
			//        return AbilityNames.Archer.FireShelling;
			//    }
			//}

			//// если хватает ресурса - используем прицельный
			//if (context.Actor.CurrentResource >= context.Actor.GetAbilityParams(AbilityNames.Archer.AimedShot).BaseParams.ResourceCost)
			//    return AbilityNames.Archer.AimedShot;

			//// используем обычный выстрел
			return AbilityNames.Archer.StandardShot;
		}
	}
}


/*
Прицельный с бонусом х1.6 (>50% хп) и х2(вне боя) и (+73)амулет. Шанс (20%) прокнуть бафф обстрела на 4сек
Поджигание: дебафф на 12 сек
Автоатака (+4% входящего урона) на 9 сек
Автоатака (+4% входящего урона) второй стак
Автоатака (+4% входящего урона) третий стак
Пробивающий (x6 (или x7?)), снятие горения.
Поджигание: дебафф на 12 сек
Прицельный


Условия:
хп цели > X  -> увелич урон прицельного на %
хп цели > X -> увелич урон поджигания на %
вне боя -> увелич урон прицельного на %
бафф (на урон поджигания (талант)) -> увелич урон поджигания на %
дебафф (на + урон) -> увелич урон всего на %


Бафф(дебафф)
1. есть список способностей на которые он влияет. (типы влияний: увеличить урон на X%; снизить стоимость на X/X%)
2. урон который он наносит
3. частота нанесения урона

Способность
1. наносит урон
2. вешает дебафф (есть список дебаффов которые она вешает)
3. вешает дебафф
4. тратит бафф/дебафф и что-то делает


Стоит ли делать баффы и способность независимыми 'самодействующими' экземплярами или сделать общий менеджер, который будет делать все переходы баффов и т.п.?

Баффы для моделирования:
1. Бафф прицельный вне боя: спадает после любой атаки, снижает время каста до 0. Увеличивает урон прицельного на 100%.
2. Дебафф 'горение'. Наносит урон. Висит 12 сек.
3. Дебафф 'xxx'. Увеличивает входящий урон на 4% за стак. стакается 3 раза. Висит 9 сек.
4. Дебафф 'yyy'. Не позволяет снять горение пронзающим. Висит 10 сек.
5. Бафф: (хп цели > 50%). Увеличивает урон горения и прицельного. Висит пока хп цели > 50. (либо висит все время, но постоянно 
учитывается хп цели)
6. Бафф 'обстрел': Стоимость обстрела снижена до 0, висит 4 сек.
7. Бафф снижает стоимость переключения

Способности для моделирования:
1. Прицельный. Шанс прокнуть 'обстрел'
2. Автоатака. Вешает дебафф 'xxx'
3. Огненная стрела - вешает дебафф 'горение'. Шанс прокнуть 'обстрел'
4. Пронзающий. Снимает 'горение' и вешает дебафф (если нет дебаффа).

Ротация: 

Если начало боя и есть ресурс -> Прицельный.
Если висит дебафф обстрела и на цели 3 стака 'ххх' -> обстрел
Если висит дебафф обстрела и нет 3 стаков 'xxx' и мы можем настакать -> автоатака
Если висит дебафф обстрела и мы не успеем настакать -> обстрел
Если нет Горения и есть ресурс -> Огненная стрела
Если горение, 3 стака и нет дебаффа и мы успеем снять горение -> пронзающий
Автоатака

*/


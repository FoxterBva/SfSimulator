using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	/// <summary>
	/// Represents constant actor stats
	/// TODO: possibly add possibility to emulate externals stats increase (like from Alchemist/HS)
	/// </summary>
	public class ActorStats
	{
		/// <summary>
		/// Могущество
		/// </summary>
		public double Might { get; set; }

		/// <summary>
		/// Выносливость
		/// </summary>
		public double Stamina { get; set; }

		/// <summary>
		/// Сила
		/// </summary>
		public double Str { get; set; }

		/// <summary>
		/// Удача
		/// </summary>
		public double Lucky { get; set; }

		/// <summary>
		/// Отвага
		/// </summary>
		public double Brave { get; set; }

		/// <summary>
		/// Дух
		/// </summary>
		public double Spirit { get; set; }

		/// <summary>
		/// Шанс крита. Шанс нанести критический урон
		/// </summary>
		public double CritChancePercent { get; set; }

		/// <summary>
		/// Сокрушающий. Шанс что базовый урон удвоится
		/// </summary>
		public double CrushingChancePercent { get; set; }
		
		/// <summary>
		/// Вспыльчивость. Увеличивает доп. урон и дает шанс, что он станет максимальным
		/// </summary>
		public double TestinessPercent { get; set; }
		
		/// <summary>
		/// Восстановление разряда. Увеличивает импульсный урон и сокращает время перезарядки
		/// </summary>
		public double ImpulsePercent { get; set; }
		
		/// <summary>
		/// Точность. Увеличивает влияние силы на базовый урон и приближает нижнюю границу.
		/// </summary>
		public double PrecisePercent { get; set; }
		
		/// <summary>
		/// Массивность. Добавляет к могуществу долю от выносливости
		/// </summary>
		public double SolidityPercent { get; set; }

		public double StrBonus { get; set; }

		public double SpiritBonus { get; set; }

		public double LuckyBonus { get; set; }

		public double BraveBonus { get; set; }
	}
}

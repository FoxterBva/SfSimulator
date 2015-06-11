﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon
{
	public class Constants
	{
		public static double MightCoeff = 0.0968;
		public static double StrCoeff = 0.2;
		public static double BraveCoeff = 0.2;
		public static double StaminaCoeff = 10;
	}

	public class AbilityNames
	{
		//public static readonly string PaladinLKM = "Удар Праведника";
		//public static readonly string PaladinLKMx2 = "Удар Карателя";
		//public static readonly string PaladinLKMx3 = "Удар Судьи";
		//public static readonly string PaladinLKMx4 = "Меч Правосудия";
		//public static readonly string PaladinLKMx2PKM = "Карающая молния";
		//public static readonly string PaladinPKM = "Волна гнева";
		//public static readonly string Paladin4 = "Яростный приговор";

		public static class Paladin
		{
			public static readonly string LKM = "Удар Праведника";
			public static readonly string LKMx2 = "Удар Карателя";
			public static readonly string LKMx3 = "Удар Судьи";
			public static readonly string LKMx4 = "Меч Правосудия";
			public static readonly string LKMx2PKM = "Карающая молния";
			public static readonly string PKM = "Волна гнева";
			public static readonly string Ability4 = "Яростный приговор";
		}

		public static class Archer
		{
			public static readonly string AimedShot = "Прицельный выстрел";
			public static readonly string LongAimedShot = "Прицельный выстрел(макс)";
			public static readonly string StandardShot = "Обычный выстрел";
			public static readonly string FireArrow = "Горящая стрела";
			public static readonly string PiercingShot = "Пронзающий выстрел";
		}
	}
}
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
			// �������� � �����������
			//if (previousUsedAbility == null)
			//    return AbilityNames.Archer.LongAimedShot;

			// ���� �� ����� ������� - ������ �������� ������
			var fireDot = context.Actor.Buffs.FirstOrDefault(b => b.Buff.Name == BuffNames.Archer.BurningDot);
			if (fireDot == null)
			{
				var p = context.Actor.GetAbilityParams(AbilityNames.Archer.FireArrow);

				if (context.Actor.CurrentResource >= p.BaseParams.ResourceCost)
					return AbilityNames.Archer.FireArrow;
			}

			// ���� ����� ���� ����������� �������� � ������� ����� ������ ���������� ����� ���������� �������
			//if (context.Actor.Buffs.FirstOrDefault(b => b.Buff.Name == BuffNames.Archer.FireShellingBuff) != null)
			//{ 
			//    var shelling = context.Actor.GetAbilityParams(AbilityNames.Archer.FireShelling);
			//    if (fireDot != null && fireDot.EndTime > context.CurrentTime + shelling.BaseParams.TotalCastTime)
			//    {
			//        return AbilityNames.Archer.FireShelling;
			//    }
			//}

			//// ���� ������� ������� - ���������� ����������
			//if (context.Actor.CurrentResource >= context.Actor.GetAbilityParams(AbilityNames.Archer.AimedShot).BaseParams.ResourceCost)
			//    return AbilityNames.Archer.AimedShot;

			//// ���������� ������� �������
			return AbilityNames.Archer.StandardShot;
		}
	}
}


/*
���������� � ������� �1.6 (>50% ��) � �2(��� ���) � (+73)������. ���� (20%) �������� ���� �������� �� 4���
����������: ������ �� 12 ���
��������� (+4% ��������� �����) �� 9 ���
��������� (+4% ��������� �����) ������ ����
��������� (+4% ��������� �����) ������ ����
����������� (x6 (��� x7?)), ������ �������.
����������: ������ �� 12 ���
����������


�������:
�� ���� > X  -> ������ ���� ����������� �� %
�� ���� > X -> ������ ���� ���������� �� %
��� ��� -> ������ ���� ����������� �� %
���� (�� ���� ���������� (������)) -> ������ ���� ���������� �� %
������ (�� + ����) -> ������ ���� ����� �� %


����(������)
1. ���� ������ ������������ �� ������� �� ������. (���� �������: ��������� ���� �� X%; ������� ��������� �� X/X%)
2. ���� ������� �� �������
3. ������� ��������� �����

�����������
1. ������� ����
2. ������ ������ (���� ������ �������� ������� ��� ������)
3. ������ ������
4. ������ ����/������ � ���-�� ������


����� �� ������ ����� � ����������� ������������ '����������������' ������������ ��� ������� ����� ��������, ������� ����� ������ ��� �������� ������ � �.�.?

����� ��� �������������:
1. ���� ���������� ��� ���: ������� ����� ����� �����, ������� ����� ����� �� 0. ����������� ���� ����������� �� 100%.
2. ������ '�������'. ������� ����. ����� 12 ���.
3. ������ 'xxx'. ����������� �������� ���� �� 4% �� ����. ��������� 3 ����. ����� 9 ���.
4. ������ 'yyy'. �� ��������� ����� ������� ����������. ����� 10 ���.
5. ����: (�� ���� > 50%). ����������� ���� ������� � �����������. ����� ���� �� ���� > 50. (���� ����� ��� �����, �� ��������� 
����������� �� ����)
6. ���� '�������': ��������� �������� ������� �� 0, ����� 4 ���.
7. ���� ������� ��������� ������������

����������� ��� �������������:
1. ����������. ���� �������� '�������'
2. ���������. ������ ������ 'xxx'
3. �������� ������ - ������ ������ '�������'. ���� �������� '�������'
4. ����������. ������� '�������' � ������ ������ (���� ��� �������).

�������: 

���� ������ ��� � ���� ������ -> ����������.
���� ����� ������ �������� � �� ���� 3 ����� '���' -> �������
���� ����� ������ �������� � ��� 3 ������ 'xxx' � �� ����� ��������� -> ���������
���� ����� ������ �������� � �� �� ������ ��������� -> �������
���� ��� ������� � ���� ������ -> �������� ������
���� �������, 3 ����� � ��� ������� � �� ������ ����� ������� -> ����������
���������

*/


using JetBrains.Annotations;

namespace MFramework.Common.Core.Extensions
{
    [PublicAPI]
	public static class RussianLanguageExtensions
	{
		/// <summary>
		/// ���������� ����� ����� ��� ���������� ����������. ��������, 41 ����������, 42 ����������, 45 ���������.
		/// </summary>
		/// <param name="value">�����, � �������� ��������� ���������������.</param>
		/// <param name="oneForm">����� ����� ��� ������������� �����.</param>
		/// <param name="twoForm">����� ����� ��� ����.</param>
        /// <param name="fiveForm">����� ����� ��� ����.</param>
		/// <returns>����� � ����� ������ ���������� ����� ����������������.</returns>
		public static string ToString(this int value, string oneForm, string twoForm, string fiveForm)
		{
            var significantValue = value % 100;

            if (significantValue >= 10 && significantValue <= 20)
                return string.Format("{0} {1}", value, fiveForm);

            var lastDigit = value % 10;
            if (lastDigit == 1)
                return string.Format("{0} {1}", value, oneForm);

            if (lastDigit == 2 || lastDigit == 3 || lastDigit == 4)
                return string.Format("{0} {1}", value, twoForm);

            return string.Format("{0} {1}", value, fiveForm);		    
		}
	}
}
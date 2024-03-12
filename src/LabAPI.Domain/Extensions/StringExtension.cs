namespace LabAPI.Domain.Extensions;

public static class StringExtension
{
	public static string EncodePolishLetterAndWhiteChars(this string str)
	{
		var s = str
			.Replace('ł', 'l')
			.Replace('ą', 'a')
			.Replace('ę', 'e')
			.Replace('ó', 'o')
			.Replace('ć', 'c')
			.Replace('ż', 'z')
			.Replace('ź', 'z')
			.Replace('ś', 's')
			.Replace('ń', 'n')
			.Replace(' ', '-')
			.Where(c => char.IsLetterOrDigit(c) || c == '-')
			.ToArray();
		return new string(s);
	}

}
using System.Reflection;

namespace LabAPI.Domain.Common;

public abstract class Enumeration<T> : IEquatable<Enumeration<T>>
	where T : Enumeration<T>
{
	private static readonly Dictionary<int, T> Enumerations = CreateEnumerations();
	public int Id { get; protected init; }
	public string Name { get; protected init; }

	protected Enumeration(int id, string name)
	{
		Id = id;
		Name = name;
	}

	public static T? FromValue(int value)
	{
		return Enumerations.GetValueOrDefault(value);
	}

	public static T? FromName(string name)
	{
		return Enumerations.Values
			.SingleOrDefault(r => r.Name == name);
	}
	
	public bool Equals(Enumeration<T>? other)
	{
		if (other is null)
			return false;
		return GetType() == other.GetType() && Id == other.Id;
	}

	public override bool Equals(object? obj)
	{
		return obj is Enumeration<T> other && Equals(other);
	}

	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}

	private static Dictionary<int, T> CreateEnumerations()
	{
		var enumerationType = typeof(T);
		var fieldsForType = enumerationType
			.GetFields(
				BindingFlags.Public |
				BindingFlags.Static |
				BindingFlags.FlattenHierarchy
			)
			.Where(info =>
				enumerationType.IsAssignableFrom(info.FieldType))
			.Select(info =>
				(T)info.GetValue(default)!);
		return fieldsForType.ToDictionary(r => r.Id);
	}

	public static List<T> GetValues()
	{
		var enumerationType = typeof(T);
		var fieldsForType = enumerationType
			.GetFields(
				BindingFlags.Public |
				BindingFlags.Static |
				BindingFlags.FlattenHierarchy
			)
			.Where(info =>
				enumerationType.IsAssignableFrom(info.FieldType))
			.Select(info =>
				(T)info.GetValue(default)!);
		return fieldsForType.ToList();
	}
}
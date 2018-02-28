class Person(string name, DateTime birthday)
{
	public string Name { get; } = name;
	
	public DateTime BirthDay { get; } = birthday;
	
	public int Age() => DateTime.Today.Substract(birthday).Days / 365;
}
using System.Text.Json;

namespace MAES.Core;

public abstract class AppData
{
	static readonly JsonSerializerOptions options = new () { WriteIndented = true };

    string filePath { get; set; } = "";

	public static T Load<T>(string fileName) where T : AppData, new()
	{
		T result;

		try
		{
			result = JsonSerializer.Deserialize<T>(File.ReadAllText(fileName)) ?? new T();
		}
		catch (Exception)
		{
			result = new T();
            File.WriteAllText(fileName, JsonSerializer.Serialize(result, options));
		}

		result.filePath = fileName;

		return result;
	}

	public async Task SaveChanges() => File.WriteAllText(filePath, JsonSerializer.Serialize(this, GetType(), options));
}
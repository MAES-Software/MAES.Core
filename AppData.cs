using System.Collections;
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

public class AppDataList<T> : AppData, IList<T>
{
    public T this[int index] { get => values[index]; set => values[index] = value; }

    public int Count => values.Count;

    public bool IsReadOnly => false;

    readonly List<T> values = [];

    public void Add(T item) => values.Add(item);
    public void Clear() => values.Clear();
    public bool Contains(T item) => values.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => values.CopyTo(array, arrayIndex);
    public IEnumerator<T> GetEnumerator() => values.GetEnumerator();
    public int IndexOf(T item) => values.IndexOf(item);
    public void Insert(int index, T item) => values.Insert(index, item);
    public bool Remove(T item) => values.Remove(item);
    public void RemoveAt(int index) => values.RemoveAt(index);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class AppDataDictionary<T, U> : AppData, IDictionary<T, U> where T : notnull
{
    readonly Dictionary<T, U> values = [];

    public U this[T key] { get => values[key]; set => values[key] = value; }
    public ICollection<T> Keys => values.Keys;
    public ICollection<U> Values => values.Values;
    public int Count => values.Count;
    public bool IsReadOnly => false;
    public void Add(T key, U value) => values.Add(key, value);
    public void Add(KeyValuePair<T, U> item) => values.Add(item.Key, item.Value);
    public void Clear() => values.Clear();
    public bool Contains(KeyValuePair<T, U> item) => values.Contains(item);
    public bool ContainsKey(T key) => values.ContainsKey(key);
    public void CopyTo(KeyValuePair<T, U>[] array, int arrayIndex) => values.ToArray().CopyTo(array, arrayIndex);
    public IEnumerator<KeyValuePair<T, U>> GetEnumerator() => values.GetEnumerator();
    public bool Remove(T key) => values.Remove(key);
    public bool Remove(KeyValuePair<T, U> item) => values.Remove(item.Key);
    public bool TryGetValue(T key, out U value)
    {
        bool result = values.TryGetValue(key, out var val);
        value = val;
        return result;
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
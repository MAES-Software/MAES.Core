namespace MAES.Core;

[AttributeUsage(AttributeTargets.Property)]
public abstract class ViewBaseAttribute : Attribute
{
    public int Index = int.MaxValue;
    public string? Label = null;
    public bool Searchable = true;
    public bool Sortable = true;
    public bool Filterable = false;
	public bool InitialSort;
    public bool Descending;
}

public class TextViewAttribute : ViewBaseAttribute { }

[AttributeUsage(AttributeTargets.Property)]
public abstract class InputBaseAttribute : Attribute
{
    public string? Group { get; set; } = null;
    public int Index { get; set; } = int.MaxValue;
    public string? Label { get; set; } = null;
    public bool Readonly { get; set; } = false;
    public string Hint { get; set; } = "";
}

public class TextInputAttribute : InputBaseAttribute
{
    public bool FullWidth { get; set; } = false;
    public int Lines { get; set; } = 1;
}

[AttributeUsage(AttributeTargets.Property)]
public class DisplayMemberAttribute : Attribute { }
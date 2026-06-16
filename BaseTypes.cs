using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAES.Core;

public abstract class Entity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[MaxLength(64)]
	public string Id { get; set; } = Guid.NewGuid().ToString();
}

public class NamedEntity : Entity
{
	[Required(ErrorMessage = "Name is required")]
	[DisplayMember]
	[TextView(Index = 0, Filterable = true)]
	[TextInput(Index = 0, FullWidth = true)]
	[MaxLength(128)]
	public string Name { get; set; } = "";
}

public class DescribedEntity : Entity
{
	[TextView(Index = 1, Filterable = true)]
	[TextInput(Index = 1, FullWidth = true, Lines = 3)]
	[MaxLength(512)]
	public string Description { get; set; } = "";
}

public class JedinicaMjere(string kod, string naziv, string simbol)
{
    public string Kod { get; set; } = kod;
    public string Naziv { get; set; } = naziv;
    public string Simbol { get; set; } = simbol;
}
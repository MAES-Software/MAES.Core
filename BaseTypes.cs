﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAES.Core;

public abstract class Entity
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
}

public class NamedEntity : Entity
{
	[Required(ErrorMessage = "Polje \"Naziv\" mora biti popunjeno")]
	[DisplayMember]
	[TextView(Index = 0, Filterable = true)]
	[TextInput(Index = 0, FullWidth = true)]
	[MaxLength(128)]
	public string Naziv { get; set; } = "";
}

public class JedinicaMjere(string kod, string naziv, string simbol)
{
    public string Kod { get; set; } = kod;
    public string Naziv { get; set; } = naziv;
    public string Simbol { get; set; } = simbol;
}
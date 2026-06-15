# MAES.Core

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)
![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-512bd4?logo=dotnet)
![C# 11](https://img.shields.io/badge/C%23-11-green?logo=csharp)
![Build Status](https://img.shields.io/badge/build-passing-brightgreen)

## 📋 Pregled

**MAES.Core** je lightweight .NET Standard 2.0 biblioteka koja pruža osnovnu infrastrukturu za rad sa entitetima, datotekama i validacijom. Dizajnirana je za distribuirane sisteme koji trebaju jednostavnu serijalizaciju podataka, prilagođene atribute za prikaz i unos podataka, te lokalizovane validatore.

Ovo je **jezgrena biblioteka** (Core library) koja nudi:
- 🔄 Upravljanje persistencijom podataka kroz JSON serijalizaciju
- 📦 Tipske entitete sa podrškavanjem baze podataka
- 🎨 Prilagođeni atributi za UI prikaz i unos
- ✅ Prilagođeni validatori (OIB, boolean validacija)
- 📊 Standardnu listu jedinica mjere (JedinicaMjere)

## ✨ Ključne Karakteristike

### AppData - Upravljanje Podacima
- Apstraktna klasa za učitavanje i čuvanje objekata u JSON format
- Automatska konverzija podataka sa rukovanjem greškama
- Async podrška za čuvanje podataka
- Pretty-print JSON formatiranje

### Bazne Klase Entiteta
- **Entity**: Osnovna klasa sa ID poljem (auto-increment)
- **NamedEntity**: Proširenje sa poljem `Naziv` (obavezno, max 128 karaktera)
- **JedinicaMjere**: Predstava jedinica mjere sa kodom, nazivom i simbolom

### Prilagođeni Atributi

#### View Atributi
- `TextViewAttribute`: Za prikaz tekstualnih svojstava
  - Podrška za sortiranje, pretragu i filtriranje
  - Prilagođivi indeks i labela
- `DisplayMemberAttribute`: Označava svojstvo kao primarni prikaz entiteta

#### Input Atributi
- `TextInputAttribute`: Za unos tekstualnih podataka
  - Opcije: FullWidth, broj redova, samo čitanje
  - Grupiranje, labele i pomoćni tekst
- `InputBaseAttribute`: Bazna klasa za sve input atribute

### Validatori
- **OIBValidator**: Validacija hrvatskog OIB (Osobni identifikacijski broj)
  - Provjera dužine i formata
  - Matematička provjera kontrolne cifre
  - Podrška za strane OIB-e
- **MustBeTrueAttribute**: Zahtijeva boolean vrijednost `true`

### Konstante
- **Kompletna lista JedinicaMjere** sa 100+ jedinica mjere
- Najčešće korištene u logistici i transportu
- Višejezičke oznake (engleske i lokalne)

## 🚀 Instalacija

### Zahtjevi
- .NET Standard 2.0 ili više (.NET 5+, .NET Framework 4.7.2+)
- C# 11 (ili novije za implicit usings)

### NuGet Paketi
Projekat ovisi od:
```xml
<PackageReference Include="System.Text.Json" Version="10.0.9" />
<PackageReference Include="System.ComponentModel.Annotations" Version="5.0.0" />
```

### Iz izvornog koda
```bash
git clone <repository-url>
cd MAES.Core
dotnet build
```

## 💻 Primjeri Upotrebe

### Učitavanje i Čuvanje Podataka
```csharp
// Kreiraj klasu koja nasleđuje AppData
public class KorisnikPodaci : AppData
{
    public string Ime { get; set; } = "";
    public string OIB { get; set; } = "";
}

// Učitaj podatke iz datoteke
var korisnik = KorisnikPodaci.Load<KorisnikPodaci>("korisnik.json");
korisnik.Ime = "Petar";

// Spremi promjene
korisnik.SaveChanges(); // Čuva u JSON format
```

### Rad sa Entitetima
```csharp
// Naslijeđivanje NamedEntity
public class Proizvod : NamedEntity
{
    [TextInput(Index = 1, FullWidth = true)]
    public string Opis { get; set; } = "";
    
    [Required]
    public decimal Cena { get; set; }
}

var proizvod = new Proizvod { Naziv = "Laptop", Cena = 500m };
```

### Validacija
```csharp
public class Kompanija : Entity
{
    [OIBValidator]
    public string OIB { get; set; } = "";
    
    [MustBeTrue(ErrorMessage = "Morate prihvatiti uvjete korištenja")]
    public bool UvjetiKoristenja { get; set; }
}
```

### Korišćenje JedinicaMjere
```csharp
// Sve dostupne jedinice
foreach (var jedinica in Constants.JediniceMjere)
{
    Console.WriteLine($"{jedinica.Kod}: {jedinica.Naziv}");
}
```

## 📝 Licenca

Ovaj projekat je licenciran pod **GNU General Public License v3.0** - pogledaj [LICENSE](LICENSE) datoteku za detalje.

### Što to znači?
- ✅ Slobodno korištenje u vlastitim projektima
- ✅ Mijenjanje izvornog koda
- ⚠️ Obaveza dijeljenja izmjena pod istom licencom
- ⚠️ Obaveza uključivanja izvorne licence

## 🤝 Doprinos

Doprinos je dobrodošao! Molimo:

1. Forkiraj spremište
2. Kreiraj feature granu (`git checkout -b feature/NovaFunkcionalnost`)
3. Slijedi kod i konvencije
4. Potvr di izmjene (`git commit -m 'Dodaj novu funkcionalnost'`)
5. Prikupi u granu (`git push origin feature/NovaFunkcionalnost`)
6. Otvori Pull Request

## 📚 Napomene

- Projekt koristi **implicit using** direktive (C# 10+)
- **Null-safety** je omogućen (`<Nullable>enable</Nullable>`)
- **System.Text.Json** se koristi umjesto Newtonsoft.Json za JSON serijalizaciju

## ❓ Česta Pitanja

**P: Mogu li koristiti ovo u komercijalnim projektima?**  
O: Da, ali morate dijeliti kod na istoj GPL v3 licenci.

**P: Koja je minimalna verzija .NET-a?**  
O: .NET Standard 2.0 je kompatibilan sa .NET Framework 4.7.2+ i svim verzijama .NET Core 2.0+.

**P: Kako dodam nove validatore?**  
O: Naslijedi `ValidationAttribute` iz `System.ComponentModel.DataAnnotations` i implementiraj `IsValid()` metodu.

## 📞 Kontakt i Podrška

Za probleme, pitanja ili sugestije, molimo otvori [GitHub Issue](../../issues).

---
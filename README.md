# 🛠️ Aplicatie C# - Gestionare Produse, Servicii și Pachete

Acest proiect este o aplicație de tip consolă scrisă în C#, care permite gestionarea unei colecții de produse, servicii și pachete combinate. Include funcționalități de introducere a datelor, interogări LINQ, sortare și filtrare extensibilă pe baza interfețelor.

## ✅ Funcționalități principale

- 📥 **Introducerea datelor**:
  - Din consolă
  - Din fișier XML (`p_s.xml`)

- 📦 **Gestionarea elementelor**:
  - Produse și servicii adăugate într-un manager comun
  - Posibilitatea de a crea pachete care conțin mai multe produse/servicii

- 🔍 **Interogări LINQ**:
  - Afișare produse dintr-o categorie
  - Afișare servicii sub un anumit preț
  - Grupare elemente după categorie

- 🔃 **Sortare pachete**:
  - Pachetele sunt sortate în funcție de preț

- 🧰 **Filtrare extensibilă**:
  - Filtrare după categorie
  - Filtrare după preț maxim
  - Sistem extensibil cu interfețe (`ICriteriu`, `IFiltrare`)

## 🧱 Arhitectură

- `ProduseManager`, `ServiciiManager` – manageri pentru citirea și stocarea produselor/serviciilor
- `Pachet` – clasa care conține mai multe produse/servicii
- `ProdusAbstract` – clasă de bază pentru `Produs` și `Serviciu`
- `ICriteriu<T>` – interfață pentru definirea criteriilor de filtrare
- `IFiltrare<T>` – interfață pentru clasele care aplică filtrarea
- `FiltrareCriteriu<T>` – implementare generică a filtrării pe baza unui criteriu

## 📂 Structura proiectului


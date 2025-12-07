# Projekt Quiz WPF
---

## ✅ Wykonane zadania

### 1. Programowanie Obiektowe (OOP) ✔
Zaimplementowano klasy modelowe:
- `Quiz`
- `Pytanie`
- `Odpowiedz`

Klasy odwzorowują strukturę quizu oraz relacje 1→n.

---

### 2. Interfejsy i abstrakcja ✔
Dodano interfejsy:
- `IQuiz`
- `IQuestion`
- `IAnswer`

Każda klasa modelowa implementuje swój interfejs.

---

### 3. Generyki ✔
Utworzono klasę generyczną:

```csharp
MenedzerQuizow<TQuiz> where TQuiz : IQuiz
```
---
### 4. LINQ ✔

W projekcie wykorzystano różne zapytania LINQ:

sortowanie quizów (OrderBy)

filtrowanie przy wyszukiwaniu (Where)

wyszukiwanie w generics managerze

---

### 5. Entity Framework Core + Baza danych ✔

Projekt korzysta z SQL Server (LocalDB) oraz EF Core.

Zainstalowane pakiety:

- Microsoft.EntityFrameworkCore

- Microsoft.EntityFrameworkCore.SqlServer

- Microsoft.EntityFrameworkCore.Design

- Microsoft.EntityFrameworkCore.Tools

Wykonano:

konfigurację kontekstu QuizContext

migracje (Add-Migration, Update-Database)

mapowanie encji na tabele

pełną obsługę zapisu, odczytu, aktualizacji i usuwania danych

---

### 6. CRUD (Create, Read, Update, Delete) ✔
**Create**

Dodawanie nowego quizu z pola tekstowego – przycisk „Dodaj quiz”.

**Read**

Wczytywanie quizów z bazy przy starcie oraz odświeżaniu listy.

**Update**

Zmiana tytułu zaznaczonego quizu – przycisk „Zapisz zmiany”.

**Delete**

Usuwanie quizu – przycisk „Usuń zaznaczony”.

---

### 7. WPF – Interfejs użytkownika ✔

Zrealizowany interfejs zawiera:

- pole wpisywania tytułu quizu

- pole wyszukiwania (dynamiczne filtrowanie LINQ)

- ListBox z quizami (DisplayMemberPath = "Tytul")

przyciski:

- Dodaj quiz

- Odśwież listę

- Zapisz zmiany

- Usuń zaznaczony

Cała logika interfejsu znajduje się w MainWindow.xaml.cs.

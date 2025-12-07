# Aplikacja Quiz - Wyszukiwarka Quizów

## Informacje o projekcie

**Wersja .NET:** .NET 8.0

**Typ projektu:** WPF App (.NET) w C#

**Baza danych:** SQL Server (LocalDB)

**Wersje pakietów EF Core:** 8.0.22

---

## Wymagane pakiety NuGet

Aby zainstalować wymagane pakiety NuGet, wykonaj jedną z poniższych opcji:

### Opcja 1: Package Manager Console (PMC) w Visual Studio

Otwórz Package Manager Console (Tools → NuGet Package Manager → Package Manager Console) i wykonaj następujące polecenia:

```powershell
Install-Package Microsoft.EntityFrameworkCore -Version 8.0.22
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.22
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.22
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.22
```

### Opcja 2: NuGet Package Manager (GUI)

1. Kliknij prawym przyciskiem myszy na projekt `QuizApp` w Solution Explorer
2. Wybierz "Manage NuGet Packages..."
3. Przejdź do zakładki "Browse"
4. Wyszukaj i zainstaluj następujące pakiety:
   - `Microsoft.EntityFrameworkCore` (wersja 8.0.22)
   - `Microsoft.EntityFrameworkCore.SqlServer` (wersja 8.0.22)
   - `Microsoft.EntityFrameworkCore.Tools` (wersja 8.0.22)
   - `Microsoft.EntityFrameworkCore.Design` (wersja 8.0.22)

### Opcja 3: .NET CLI

Otwórz terminal w folderze projektu i wykonaj:

```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.22
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.22
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.22
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.22
```

---

## Tworzenie bazy danych

### Metoda 1: Automatyczne tworzenie (EnsureCreated)

Aplikacja automatycznie utworzy bazę danych przy pierwszym uruchomieniu dzięki metodzie `EnsureCreated()` w pliku `App.xaml.cs`.

**Uwaga:** Ta metoda jest prosta, ale w produkcji lepiej używać migracji!

### Metoda 2: Migracje Entity Framework Core (zalecane dla produkcji)

Jeśli chcesz użyć migracji Entity Framework Core (wymagane przez niektóre wymagania uniwersyteckie):

1. **Otwórz Package Manager Console** w Visual Studio

2. **Utwórz migrację:**
   ```powershell
   Add-Migration InitialCreate
   ```

3. **Zastosuj migrację do bazy danych:**
   ```powershell
   Update-Database
   ```

4. **Aby utworzyć nową migrację po zmianach w modelach:**
   ```powershell
   Add-Migration NazwaMigracji
   Update-Database
   ```

---

## Struktura projektu

```
QuizApp/
├── Models/              # Modele danych i interfejsy
│   ├── IQuiz.cs        # Interfejs dla Quiz
│   ├── IQuestion.cs    # Interfejs dla Question
│   ├── IAnswer.cs      # Interfejs dla Answer
│   ├── Quiz.cs         # Klasa Quiz (implementuje IQuiz)
│   ├── Question.cs     # Klasa Question (implementuje IQuestion)
│   └── Answer.cs       # Klasa Answer (implementuje IAnswer)
├── Data/               # Warstwa dostępu do danych
│   ├── QuizDbContext.cs      # Kontekst Entity Framework Core
│   ├── IRepository.cs        # Generyczny interfejs repozytorium
│   └── Repository.cs         # Generyczna implementacja repozytorium
├── Services/           # Warstwa logiki biznesowej
│   └── QuizService.cs  # Serwis do zarządzania quizami i pytaniami
├── App.xaml            # Plik konfiguracyjny aplikacji WPF
├── App.xaml.cs         # Kod aplikacji WPF
├── MainWindow.xaml     # Główne okno aplikacji (wyszukiwarka)
├── MainWindow.xaml.cs  # Kod głównego okna
├── NameInputDialog.xaml     # Okno do wprowadzenia imienia gracza
├── NameInputDialog.xaml.cs  # Kod okna wprowadzenia imienia
├── QuizSolvingWindow.xaml   # Okno do rozwiązywania quizu
└── QuizSolvingWindow.xaml.cs # Kod okna rozwiązywania quizu
```

---

## Funkcjonalności zaimplementowane

### 1. Programowanie obiektowe z klasami
- ✅ `Quiz` - klasa reprezentująca quiz
- ✅ `Question` - klasa reprezentująca pytanie
- ✅ `Answer` - klasa reprezentująca odpowiedź

### 2. Interfejsy i abstrakcja
- ✅ `IQuiz` - interfejs dla Quiz
- ✅ `IQuestion` - interfejs dla Question
- ✅ `IAnswer` - interfejs dla Answer
- ✅ Klasy implementują odpowiednie interfejsy

### 3. Generyki
- ✅ `IRepository<T>` - generyczny interfejs repozytorium
- ✅ `Repository<T>` - generyczna implementacja repozytorium dla EF Core

### 4. WPF
- ✅ Aplikacja desktopowa WPF z prostym interfejsem użytkownika
- ✅ DataGrid do wyświetlania wyników wyszukiwania quizów
- ✅ Pole wyszukiwania z przyciskiem "Szukaj" (używa LINQ)
- ✅ Okno do rozwiązywania quizów z nawigacją między pytaniami
- ✅ Zapis wyników do pliku tekstowego z datą ukończenia

### 5. Entity Framework Core
- ✅ Konfiguracja SQL Server (LocalDB)
- ✅ `QuizDbContext` z konfiguracją relacji
- ✅ Operacje CRUD dla quizów i pytań (dostępne w kodzie `QuizService`, nie w GUI):
  - **CREATE:** Dodawanie nowych quizów i pytań (przez migracje/seed data)
  - **READ:** Pobieranie quizów i pytań z bazy danych (używane w wyszukiwarce)
  - **UPDATE:** Aktualizacja istniejących quizów i pytań (dostępne w kodzie)
  - **DELETE:** Usuwanie quizów i pytań (dostępne w kodzie)
- ✅ Seed data - 3 przykładowe quizy dodawane automatycznie przy pierwszym uruchomieniu

### 6. LINQ
- ✅ Przykład użycia LINQ w metodzie `SearchQuizzesByTitle()` w klasie `QuizService`
- ✅ Wyszukiwanie quizów po tytule używając `Find()` z wyrażeniem lambda

---

## Jak uruchomić projekt

1. **Zainstaluj wymagane pakiety NuGet** (patrz sekcja powyżej)

2. **Upewnij się, że masz zainstalowany SQL Server LocalDB:**
   - LocalDB jest zwykle dołączony do Visual Studio
   - Jeśli nie masz, możesz zainstalować SQL Server Express z LocalDB

3. **Otwórz projekt w Visual Studio:**
   - Otwórz plik `QuizAppSolution.sln`

4. **Skompiluj projekt:**
   - Naciśnij `F5` lub wybierz "Debug → Start Debugging"

5. **Baza danych zostanie utworzona automatycznie** przy pierwszym uruchomieniu

---

## Jak używać aplikacji

Aplikacja pozwala na wyszukiwanie i rozwiązywanie quizów. Quizy są dodawane do bazy danych przez migracje (zobacz plik `JAK_DODAC_QUIZY.md`).

1. **Wyszukiwanie quizów:**
   - Wprowadź tekst w polu wyszukiwania
   - Kliknij "Szukaj" (używa LINQ do wyszukiwania)
   - Kliknij "Pokaż wszystkie", aby zobaczyć wszystkie quizy z bazy danych

2. **Rozwiązywanie quizu:**
   - Wybierz quiz z listy (kliknij na niego)
   - Kliknij przycisk "Rozwiąż Quiz" lub kliknij dwukrotnie na quiz
   - Wprowadź swoje imię w oknie dialogowym
   - Odpowiadaj na pytania wybierając jedną z odpowiedzi
   - Używaj przycisków "Następne" i "Poprzednie" do nawigacji
   - Po zakończeniu quizu wynik zostanie zapisany do pliku `wyniki.txt`

3. **Wyniki:**
   - Wszystkie wyniki są zapisywane do pliku `wyniki.txt` w folderze aplikacji
   - Format: `Imię | Tytuł Quizu | Wynik (Procent) | Data i Czas`
   - Przykład: `Jan | Historia Polski | 2/2 (100.0%) | 2024-01-15 14:30:25`

4. **Dodawanie quizów:**
   - Quizy są dodawane przez migracje bazy danych (nie przez GUI)
   - Zobacz plik `JAK_DODAC_QUIZY.md` aby dowiedzieć się, jak dodać nowe quizy
   - Przy pierwszym uruchomieniu aplikacja automatycznie dodaje 3 przykładowe quizy

---

## Uwagi dla nauczyciela

- Kod jest prosty i zawiera wiele komentarzy po polsku
- Wszystkie wymagania uniwersyteckie zostały zaimplementowane
- Projekt używa .NET 8.0 i najnowszych stabilnych wersji pakietów EF Core
- Baza danych używa SQL Server LocalDB (nie SQLite)
- LINQ jest użyte w metodzie `SearchQuizzesByTitle()` w klasie `QuizService`
- Generyki są użyte w `IRepository<T>` i `Repository<T>`
- Wszystkie klasy implementują odpowiednie interfejsy

---

## Rozwiązywanie problemów

### Problem: Błąd połączenia z bazą danych

**Rozwiązanie:** Upewnij się, że SQL Server LocalDB jest zainstalowany. Możesz sprawdzić to w SQL Server Management Studio lub zmienić connection string w pliku `QuizDbContext.cs`.

### Problem: Błąd kompilacji - brakuje pakietów NuGet

**Rozwiązanie:** Zainstaluj wszystkie wymagane pakiety NuGet (patrz sekcja "Wymagane pakiety NuGet" powyżej).

### Problem: Baza danych nie jest tworzona

**Rozwiązanie:** Sprawdź, czy LocalDB działa. Możesz też użyć migracji EF Core zamiast `EnsureCreated()`.

---

## Autor

Projekt stworzony zgodnie z wymaganiami uniwersyteckimi dla aplikacji WPF z Entity Framework Core.


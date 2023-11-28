# Drzewa decyzyjne
1. Oblicz entropię, entropię warunkową, information gain, intrinsic information, gain ratio.
2. Na podstawie powyższych wartości określ, który atrybut będzie najlepszy do wykonania podziału.
3. Potencjalne problemy z information gain i gain ratio.

    Information gain 
4. W jaki sposób należy radzić sobie z wartościami ciągłymi na atrybutach?

    - posortować po wartości i zrobić splity tam gdzie zmienia się klasa (jako średnia z sąsiadów)
5. W jaki sposób radzić sobie z brakującymi wartościami w zbiorze danych?

    - pominąć przypadek uczący
    - wybrać najczęstszą wartość
    - wybrać najczęstszą wartość w klasie decyzyjnej
    - wybrać najczęstszą wartość i dodać atrybut czy wartość była oryginalnie brakująca
    - dodać przypadki uczące z każdą wartością atrybutu
    - dodać przypadki uczące z każdą wartością atrybutu w klasie
    - traktować wartość null jako poprawną wartość atrybutu
6. Źródła niespójności w danych.

    - wszystkie atrybuty te same a inna klasa
    - błąd w etykietowaniu
    - błąd w wartości atrybutu
    - brak wartości na atrybucie
    - brakuje itotnego atrybutu w danych
    - eksperci mogą różnie klasyfikować
7. Przyczyny overfittingu.

    - zbyt skomplikowany model
    - dużo szumu w danych
8. Różnica pomiędzy training error i generalization error.

    - Training error - błąd na zbiorze uczącym, można dokładnie obliczyć
    - Błąd generalizacji - błąd jaki popełnie model dla nowych przypadków, nie da się policzbyć dokładnie, można estymować zbiorem testowym
9. Wyznaczenie optymistycznej i pesymistycznej estymaty przy błędzie uogólnienia.

    - Optymistyczna - błąda taki sam jak na zbiorze uczącym
    - Pesymistyczna - (liczba błędów + alpha * liczba liści) / liczba przypadków uczących
10. Na czym polega cross-validation?

    Dzielimi zbiór danych na k równych części i uczymy model na k-1 częściach i testujemy na jednej części.
    Powtarzami to k razy, zawsze wybierająć inną część do testu.
    - leave-one-out - k = ilość danych
    - stratified - rozkład klas na danych w każdej z k części jak taki z pełnego zbioru lub klasy są po równo
    - na każdym etapi dodatkowo dostrajamy parametry modelu
11. Pre- i post-pruning - na czym polega?
    - pre - odcinami podczas uczenia
        - wszystkie przypadki należą do tej samej klasy
        - wszystkie przypadki mają te same atrybuty (potencjalnie inne klasy jeśli dane niespójne)
        - nie da się rozdzielić tak żeby był jakiś gain
        - błąd generalizacji spadł poniżej progu
    - post - odcinami po wyuczeniu
        - sprawdzami czy błąd generalizacji spada przy odcięciu, zastępujemy klasą większościową


# k-nn
1. Jak działa algorytm k-nn?

    Trzymamy cały/część korpusu w pamięci. Sprawdzamy z użyciem miary odległości do których danych nasz przypadek ma najbliżej.
2. Na jakie elementy można mieć wpływ w działaniu algorytmu k-nn?

    Miara odległości:
     - euklidesowa
     - manhatańska
     - czebyszewa 
3. Zalety i wady k-nn

    Zalety:
     - prosty w implementacji
     - dobrze klasyfikuje dane nieliniowe
     - brak fazy treningowej
     - szybki dla małego zbioru danych

    Wady:
     - musimy trzymać dane w pamięci
     - dane muszą być odpowiednio przeskalowane
     - wymaga wybrania odpowiedniego k
4. Radzenie sobie z wartościami nominalnymi

    Jak wartość ta sama to miara odległości minimalna jak wartość inna to odległość maksymalna    
5. Jak wybór różnych wartości k może mieć wpływ na wynik (k=1, 2, ...,n)

    - k powinno być nieparzyste. Jakby było parzyste to mógłby być remis.
    - k za małe - underfiting, k za duże - overfiting

# k-means
1. Przekleństwo wymiarowości

    Od pewnego momentu dodawanie więcej atrybutów do danych utrudnia uczenie.
    
    Każdy atrybut dodaje kolejny wymiar i dane w przestrzenie stają się coraz bardziej odległe.

    Niektóre atrubuty mogę myć bezużyteczne i może nastąpić przeuczenie
2. Jak można zredukować liczbę atrybutów w problemach klasyfikacji?

    - usunąć atrybuty funkcyjnie zależne
    - info gain
3. Czym jest grupowanie?

    Inaczej analiza skupień. Wyszukujemy klastrów w danych bez etykiet
4. Jak działa algorytm?

    k-means działa poprzez umieszczenie centroidów i przesuwanie ich w kolejnych iteracjach tak żeby każdy centroid wpasował się w odpowiedni klaster.
5. Jak wybieramy początkowe centroidy?

    różne metody:
    - losowe instancje z danych
    - tak żeby były jak najdalej od siebie
6. Obliczenie jednej iteracji algorytmu
7. Jakie są warunki stopu algorytmu?

    - etykiety nie zmieniły się względem poprzedniej iteracji
    - osiągniecie maksymalnej liczby iteracji
    - połączenie obu metod
8. Zalety i wady algorytmu

    zalety:
     - prosty
     - szybki
     - zbieżny
     - uniwersalny

    wady:
     - może wpaść w nie globalne minimum, zależnie od pierwszego rozmieszczenia centroidów 
     - grupy muszą być kuliste 
     - k musi być znane
     - podatny na przypadki odstające
     - zakłada że grupy mają taką samą gęstość/wariancję
9. Selekcja atrybutów w problemach nienadzorowanych
    - Metody PCA (principal component analisis)- projekcja danych do niższego wymiaru, np. jeśli dane 3D znajdowałyby się głównie na jednej płaszczyźnie to można dokonać projekcji do 2D.
    - random projection
    - LLE (locally linear embedding)
    - LDA (Linear Discriminant Analysis)

# Regresja
1. Na czym polega regresja liniowa

    Na znalezioniu funkcji liniowej która najlepiej wpasowuje się w dane
2. Różnica między regresją wielomianową a wieloraką.

3. Czym jest obserwacja odstająca

    Obserwacja która znajduje się daleko od jakichkolwiek innych
4. W jaki sposób można uznać obserwację za odstającą.

    - z-score
    - IQR=Q3-Q1; [Q1-IQR * 1.5, Q3 + IQR * 1.5]
5. Czym jest regresja logistyczna

    Metodą klasyfikacji
6. Oblicz wartości odds i logit na podstawie danego prawdopodobieństwa.
    - odds = p / (1-p)
    - logit ln(odds(p))
Podstawy sieci neuronowych
1. Podaj definicję neuronu i sieci neuronowej.

Neuron:

$z = \sum x_i w_i + b$

$y=f(z)$

Warstwa fully connected

$z_j = \sum_i x_i * w_{ij} + b_j$

$y_j = f(z_j)$

2. Wymień najczęściej wykorzystywane funkcje aktywacji i narysuj ich wykresy.

skokowa 0 lub 1
sigmoid: S-kształt od (0, 1)
tangens hiperboliczny: S-kształt od (-1, 1)
relu $max(0, x)$

skokowa - perceptron
inna - neuron

3. Dlaczego w sieciach neuronowych stosuje się nieliniowe funkcje aktywacji? Jakie byłyby konsekwencje stosowania w całej sieci neuronowej wyłącznie neuronów z liniową funkcją aktywacji?

Kombinacja transformacji liniowych jest nadal transformacją liniową

Uczenie sieci neuronowych
1. Wykorzystaj chain rule lub multivariable chain rule w zadanym problemie.
2. Jak działa algorytm spadku gradientu oraz algorytm wstecznej propagacji błędu?
3. Podaj dla pojedynczego neuronu o jaką wartość zmieni się wartość wagi w wyniku pojedynczej iteracji algorytmu spadku gradientu.
4. W jakim celu stosuje się współczynnik uczenia (ang. learning rate)?
5. Na czym polega stochastyczny spadek gradientu?

Update gradientu po każdym przypadku uczącym zamiast po wszystkich

Warstwy sieci neuronowych
1. Opisz warstwę: w pełni połączoną, konwolucyjną, poolingu, flatten.
2. W jaki sposób można poradzić sobie z obsługą pikseli brzegowych obrazka podczas stosowania warstwy konwolucyjnej?

Padding

3. Czym jest blok ResNet, i z czego wynika lepsza efektywność uczenia sieci go wykorzystujących?

Wykorzystanie połączeń resydualnych co pozwala uniknąć zanikania gradiendu

4. Policz liczbę parametrów (wag, biasów) w zadanej architekturze neuronowej.

Regularyzacja
1. Czym jest regularyzacja i co ma na celu jej stosowanie?

Ograniczyć przeuczenie

2. W jaki sposób można przy użyciu zbioru walidacyjnego zapobiec przeuczeniu podczas uczenia sieci?

Zatrzymać uczenie, kiedy loss na walidacji przestaje spadać

3. Opisz podejścia do regularyzacji używane w notebookach: dropout, batch normalization.

dropout: wyłączenie losowych neuronów
batch norm: 

Autoenkodery
1. Zdefiniuj zadanie autoasocjacji.
2. Pojęcia enkodera i dekodera.
3. Jak wygląda klasyczna architektura autoenkodera?

Rekurencyjne sieci neuronowe
1. Jak działają rekurencyjne sieci neuronowe, i kiedy kończą przetwarzanie wejścia?
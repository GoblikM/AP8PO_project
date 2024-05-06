# Game idea
## Shrnutí
Jedná se o hru inspirovanou tématem šachů. Cílem hry je pomocí kombinací pohybů dostupných šachových figurek vyhodit cílovou figurku na nějaké pozici, která je určena levelem. Během pohybů si musí ale hráč dávat bacha na existující gravitaci v herním poli.

<p align="center">
<img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/Designer.jpeg" alt="image" width="300">
</p>

## Hlavní prvky hry
Šachy, gravitace, hledání řešení

### Cíl hry
- Hráč se snaží dosáhnout cílové pozice, která je označena červeně zbarvenou ikonkou Godot na herní šachovnici o velikosti 16x10.
  
<p align="center">
<img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/Tiles/finish_point.png" alt="image" width="50">
</p>

### Herní pole
- Hráč má k dispozici šachovnici sestávající z 16 sloupců a 10 řad.
- Každé políčko má své souřadnice.
<p align="center">
<img src="https://github.com/GoblikM/AP8PO_project/assets/75813735/5ca7ea7a-8f76-4439-a445-65c130237881" alt="image" width="300">
</p>

### Herní figurky
- Na začátku hry jsou na šachovnici umístěny šachové figurky (pěšáci, věže, střelci, jezdci, dáma, král) podle daného levelu.
- Figurky mají specifické pohybové možnosti dle běžných šachových pravidel.
<p align="center">
  <img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/pieces/pawn.png" alt="Pawn">
  <img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/pieces/rook.png" alt="Rook">
  <img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/pieces/bishop.png" alt="Bishop">
  <img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/pieces/knight.png" alt="Knight">
  <img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/pieces/king.png" alt="King">
  <img src="https://github.com/GoblikM/AP8PO_project/blob/main/assets/pieces/queen.png" alt="Queen">
</p>

### Gravitace
- Herní pole má gravitační efekt, což znamená, že pokud figurka není zablokována jinou figurkou nebo stěnou, tak padá směrem dolů, dokud nenarazí na překážku.
### Pohyby figurkami
- Hráč může vykonávat pohyby s figurkami dle běžných šachových pravidel.
- Figurka se může pohybovat dopředu, do stran nebo diagonálně, pokud není cesta blokována jinou figurkou nebo stěnou.
### Herní úrovně
- Hra obsahuje různé úrovně obtížnosti, které vyžadují od hráče různé strategie a kombinace pohybů figurkami.

## Herní mechaniky
### Interakce s figurkami
- Hráč může kliknout na šachovou figurku, kterou chce pohnout, a poté kliknout na cílovou pozici, kam chce figurku přesunout.
- Při vybrání konkrétní figurky jsou zobrazeny dostupné tahy pro danou figurku.
### Překážky
- Na herním šachovnici jsou umístěny různé překážky, jako jsou stěny nebo blokující figurky.
### Úrovně obtížnosti
- Každá úroveň přináší unikátní výzvy a překážky, které vyžadují od hráče různé strategie a přístupy k dosažení cíle. 
- Obtížnost se postupně zvyšuje od jednoduchých úrovní, které slouží jako úvod, až po složitější úrovně, které vyžadují hlubší porozumění herním mechanikám a kreativní myšlení hráče.

## Rozhraní
### Herní šachovnice
- Hlavní část obrazovky obsahuje herní šachovnici, kde hráč vidí své figurky, překážky, cílovou ikonu a může zde provádět své tahy.
### UI
#### Herní šachovnice
- Na obrazovce jsou ovládací prvky jako tlačítko pro restartování hry a návrat do hlavního menu.
- Na obrazovce se nachází text s názvem aktuálního levelu.
- Na obrazovce je vidět aktuální čas doby řešení levelu.
#### Hlavní menu
- V hlavním menu hry je tlačítko pro přechod na seznam dostupných levelů hry.
- V hlavním menu hry jsou tlačítka pro ovládání hlasitosti (zvůkových efektů, hudby)
- Tlačítko pro ukončení hry
#### Vizuální efekty
- Animace jednotlivých figurek po jejich vybrání hráčem.
- Animace jednotlivých tlačítek pro lepší interaktivitu s hráčem.
#### Zvukové efekty a hudba
- Hudba na pozadí pro doplnění atmosféry hry.
- Zvukové efekty při interakci hráče s figurkou.
- Zvukové efekty při padání figurky

# Databáze Plzeňského Prazdroje a dalších pivovarů 🍺
Předmětem práce je vytvoření databázové struktury pro jednoduchou vlastní aplikaci a následně vytvoření uživatelského rozhraní pro administraci této databázové struktury splňujícího obecné zvyklosti aplikace navržené pro snadné používání.

## Jaké bylo zadání?:
### Struktura databáze
<ul>
    <li>Databáze má nějaké téma. Z tohoto tématu pak plynou definice databázových tabulek. Téma je na Vás.</li>
    <li>Databáze obsahuje mandatorní vazbu 1:N.</li>
    <li>Databáze obsahuje vazbu N:M. Jestli bude obsahovat data v pivot tabulce - to je na Vás.</li>
    <li>Databáze obsahuje jednu tabulku typu číselník (= seznam hodnot pro sloupec jiné tabulky).</li>
    <li>Ostatní tabulky jsou zcela na Vás.</li>
</ul>

### Uživatelské rozhraní
<ul>
    <li>Cílem je vytvoření administrativní části aplikace, není tedy nutné definovat nějaký dodatečný styl aplikace, stačí Bootstrap.</li>
    <li>Všechny tabulky databáze (s možnou výjimkou číselníku) je možné naplnit z rozhraní.</li>
    <li>Všechny tabulky mají kompletní sadu CRUD funkcí. Je tedy možné položky číst, vytvářet, upravovat a mazat.</li>
    <li>Vazba 1:N je naplňována během vytváření záznamu (Studentovi je ihned přiřazena třída).</li>
    <li>Je možné definovat vazby mezi tabulkami, nemělo by to být samostatně, ale z rozhraní tabulek, které se na tomto vztahu podílejí.</li>
    <li>Nikde v aplikaci nebude uživatel nucen zadávat nějaká ID, uživatel si vždy bude moci vybrat ze seznamu člověkem čitelných hodnot.</li>
    <li>Někde bude použit přepínač Radiobutton načítající data z databáze (například z toho číselníku).</li>
    <li>V seznamu obsahu datových tabulek funguje alespoň v jednom případě filtrování a řazení.</li>
</ul>

## Databáze defaultně obsahuje informace o těchto pivovarech a jejich pivech:
<img src="https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/Logo-Assets-1.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/Asset__2-1080x1080.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_3.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__8-1080x1080.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_4.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__18-1080x1080.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_5.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/stuc-1080x1080.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_6.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__30-1080x1080.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_10.png" width="45%"></img> <img src="https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__66-1080x1080.png" width="45%"></img>

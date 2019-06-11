# The Hacker Strikes Back

This is the source code of the blog analysis on how to protect the authentication process with passwords in a decentralized solution using the hacked LinkedIn database.

## Requirements
- .NET Core 2.1
- Python 3


## Download Linkedin database
For this code to work, the first thing that is needed is to download the [hacked LinkedIn database](https://databases.today/search-nojs.php?for=linkedin) from [here](https://cdn.databases.today/linkedin_all.7z) and also the [list of cracked passwords](https://hashes.org/leaks.php?id=68) by clicking on the **Hash:Plain button**. Then the files are placed and unzipped in the project's **data folder**.


## To Run
This repository consists of different projects that have to be executed to perform all the data analysis. Therefore, from the main folder it is necessary to execute the following scripts one by one:

```bash
cd "1. split"
dotnet run

cd "../2. prepare"
python prepare.py

cd "../3. perform"
dotnet run

cd "../4. analyse"
python analyse.py
python certainty.py
```

## Result

The result of the analysis is a series of files in the analysis folder inside data that are:

### Table of the mean data analyzed *(analysis-mean.txt)*
pass_count | pass% | hash_count | hash% | certainty% | increase%
-- | -- | -- | -- | -- | --
1.609506 | 2.777225e-06 | 226381.9 | 0.3906252 | 0.0007109695 | 21,204,250

### Progress of password protections against a dictionary attack *(certainty.png)*
![Certainty Plot](/data/analysis/certainty.png)

### List of the most used passwords with their respective analysis *(analysis-unique%.txt)*
pass|hash|pass_count|pass%|hash_count|hash%|certainty%|increase%
-- | -- | -- | -- | -- | -- | -- | --
123456|141|374825|0.646765706|603443|1.041249218|62.1144002|60.99326352
linkedin|132|58725|0.101330797|282432|0.487340311|20.79261557|380.9399745
123456789|21|54712|0.09440631|288614|0.498007437|18.95680736|427.5149876
password|94|49868|0.086047922|285088|0.491923275|17.49214278|471.6852491
12345678|239|31335|0.054068975|248601|0.428964454|12.60453498|693.3652465
111111|188|27616|0.047651789|246863|0.425965511|11.18677161|793.912949
1234567|139|25687|0.044323273|251596|0.434132368|10.20962177|879.4682135
654321|72|17843|0.030788343|236457|0.408009814|7.545980876|1225.208765
0|145|16446|0.0283778|243469|0.420109117|6.754864069|1380.414691
qwerty|101|14618|0.025223561|233900|0.403597676|6.24967935|1500.082091
sunshine|169|14514|0.025044107|245801|0.424133015|5.904776628|1593.544164
abc123|108|12036|0.020768284|236833|0.408658607|5.082062044|1867.705218
666666|148|11351|0.019586307|234175|0.404072192|4.847229636|1963.034094
1234567890|199|11068|0.019097987|238202|0.411020836|4.64647652|2052.168413
123123|150|10792|0.018621745|239018|0.412428855|4.515141119|2114.7702
charlie|185|10493|0.018105816|232013|0.400341631|4.522591407|2111.1217
linked|34|9106|0.015712529|225398|0.388927357|4.039964862|2375.269053
maggie|170|9014|0.015553781|233886|0.403573518|3.854014349|2494.697138
zzzzzzzz|193|8912|0.015377779|225653|0.389367363|3.949426775|2432.013016
121212|62|8465|0.014606474|225093|0.388401076|3.760667813|2559.102185
princess|4|8379|0.01445808|229556|0.396102044|3.650089738|2639.65867
iloveyou|228|8377|0.014454629|230468|0.397675712|3.63477793|2651.199714
michael|52|8314|0.014345922|229478|0.395967454|3.623005255|2660.139524
222222|76|8025|0.013847248|229199|0.395486035|3.501324177|2756.062305
.|.|.|.|.|.|.|.


### The distribution of the small hashes *(analysis-group.txt)*
hash|#pass|#unique|max|min|mean
--|--|--|--|--|--
0|224055|140492|6183|1|1.5947883153489166
1|230267|140977|6065|1|1.6333657263241521
2|221022|140330|2302|1|1.5750160336350032
3|217710|141085|2628|1|1.5431123081830103
4|229556|141118|8379|1|1.62669538967389
5|243016|141052|6255|1|1.7228823412642147
6|219041|140286|2182|1|1.5613888770083972
7|229187|140185|3743|1|1.6348896101580055
8|224453|141202|5999|1|1.5895879661761165
9|218428|140237|2868|1|1.5575632678964895
.|.|.|.|.|.

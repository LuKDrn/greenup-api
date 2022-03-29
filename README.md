| Opération | API GreenUp | Front GreenUp
| - | -: | -: | -: | -: |
| Compilation | [![Build Status]()]() | [![Build Status]()]() |
| Déploiement Recette | [![Release Status]()]() | [![Release Status]()]() |
| Déploiement Production | [![Release Status]()]() | [![Release Status]()]() |

[[_TOC_]]
# Introduction 

Le concept a émergé lors d’un concours organisé par MyDigitalSchool durant  l'année 2021. Un hackaton regroupant 4 écoles, celles de Paris, Tours, Angers et Nantes. L’objectif ? Trouver une idée de start'up, la développer et à la fin des 5 jours de hackathon la pitcher.  Ce projet se doit d'être viable économiquement et ambitieux dans sa proposition ! 

Nous avons choisi de traiter une problématique environnementale, un sujet pour le moins actuel et urgent. En effet, durant l’année 2020 nous avons été touchés par la pandémie mais pas uniquement, des catastrophes naturelles ont fait la Une des médias, notamment les incendies en Australie, les fortes pluies à Jakarta ou encore le cyclone au Bangladesh et bien d'autre. Par envie de sensibiliser et de pousser les citoyens à l’action l’idée de Green Up nous est venue, avec la problématique « Une bonne action peut-elle être récompensée ? »


# Acteurs


## Métier

### Design
- Laurélène Maresse

### Marketing
- Maiwenn Guilbaud
- Naomi Le Bail
- Ambre Lefaucheux

## Informatique

### Développeurs
- Marc-Antoine Simon
- Lucas Derouin

# Prérequis
Pour installer un nouvel environnement de développement sous Windows, procéder comme suit :
1. Installer un IDE .NET, tel Microsoft Visual Studio ou JetBrains Rider.
1. Installer **[.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)**. Cela permettra de développer le backend et de l'exécuter sur le poste de développement.
1. Installer et valider un certificat HTTPS de développement: 
    1. Taper dans un terminal `dotnet dev-certs https --trust`.
    1. Cliquer sur "Oui" dans la fenêtre qui demande si vous validez la confiance en ce certificat.
    1. Redémarrez votre navigateur.
    1. Désormais, visiter `https://localhost:*` devrait être possible. Ce tutoriel n'est à faire qu'une seule fois.

## Première exécution locale
1. Mettre à jour (ou création) de la base de données en local.
    1. Dans un terminal rendez-vous dans le dossier **GreenUp.EntityFrameworkCore**
    1. Lancer la commande **dotnet ef database update --context GreenUpContext** ou **Update-Database -Context GreenUpContext**
    1. Si une erreur apparait et qu'en faisant un **dotnet ef -v** vous obtenez une erreur, c'est que vous avez installé une version de dotnet core supérieur ou égal à 3.0. Vous devez donc d'abord exécuter **dotnet tool install --global dotnet-ef**
1. Lancer l'application en mettant comme projet de démarrage **GreenUp.Web.MVC**

# Développement

## Solution .NET complète

La responsabilité de chaque projet suit à la fois les principes du [Domain Driven Design](https://aspnetboilerplate.com/Pages/Documents/NLayer-Architecture).
**Veillez à bien choisir quel projet impacter quand vous développez ici**. Pour vous aidez dans votre choix, lisez plus bas.

Voici les différents projets de la solution :
- `Core` : Les classes de la couche de domaine. Donc les entités et les services de domaine. C'est le coeur de l'application et la traduction de la typologie du métier et des règles du métier.
- `EntityFrameworkCore` : Les migrations EF Core Code First, les contextes de base de données, et les dépôts personnalisés (repositories). On entend par "personnalisés" le fait que cela ne soit pas les dépôts de base injectés dans `IRepository<TEntity>`, mais plutôt une classe du projet dérivant d'[EfCoreRepositoryBase](https://aspnetboilerplate.com/Pages/Documents/Entity-Framework-Core?searchKey=EfCoreRepositoryBase#application-specific-base-repository-class).
- `Application` : Les classes des services d'application. On trouve également les DTO's et les interfaces de services d'application qui seront exposés par la Web API auto-générée.
- `Web.Core` : Contient les classes nécessaires au bon fonctionnement du template ABP (ASP.NET Boilerplate ou ASP.NET Zero) SPA (Single-Page Application, comme Angular) comme MPA (Multi-Page Application, comme le template jQuery).
- `Web.Mvc` : Le projet MVC comportant les controllers et toutes les vues cshtml.

## Coding style

- **Ne pas réinventer la roue**. Explorer le code existant, le template ASP .NET Zero, le framework ASP .NET Boilerplate et les Awesome Lists ([.NET Core](https://github.com/thangchung/awesome-dotnet-core#readme) et [Javascript](https://github.com/sorrycc/awesome-javascript#readme)) avant de coder soi-même.

### Commentaires XML

- Les commentaires XML des classes et interfaces débuteront de préférence par "Représente".
- Les commentaires XML des propriétés booléennes en lecture seule débuteront de préférence par "Détermine si".
- Les commentaires XML des propriétés booléennes en lecture/écriture débuteront de préférence par "Détermine ou définit si".
- Les commentaires XML des propriétés non booléennes en lecture seule débuteront de préférence par "Obtient".
- Les commentaires XML des propriétés non booléennes en lecture/écriture débuteront de préférence par "Obtient ou définit".
- Le contenu de la balise [`<summary>`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/summary) se voudra synthétique (explications nécessaires et suffisantes). La [balise `<remarks>`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/remarks) contiendra les explications superflues, quoique intéressantes.
- Les exceptions potentiellement levées, même des méthodes appelées au sein du corps, seront, de préférence, mentionnées par [la balise `<exception>`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/exception).
- Les mentions à d'autres types, propriétés ou méthodes sera encadrée par la [balise  `<see>`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/see).

### Responsabilité des projets .NET

- D'une manière générale, le Domain Driven Design, [rappelé par la documentation d'ASP .NET Boilerplate](https://aspnetboilerplate.com/Pages/Documents/NLayer-Architecture), sera respecté.
- Les **requêtes ne concernant qu'un dépôt** (repository) seront l'occasion d'implémenter un **dépôt personnalisé** dans le projet `EntityFrameworkCore`. Voir la définition du projet `EntityFrameworkCore` plus haut.
- Les **services d'application** se contenteront de réaliser du mapping DTO-Entité, et des appels de méthodes de domaine. Les logiques et algorithmes-métier n'y ont pas leur place. L'injection de dépôts (repositories) est à éviter. **L'appel d'autres méthodes d'application est à proscrire**, ne serait-ce qu'à cause de l'audit engendré (causant ralentissement de l'application, enflement de la base de données, et bruitage des données d'audit). 
- Les **services de domaine** s'efforceront de respecter le design pattern de [responsabilité unique](https://fr.wikipedia.org/wiki/Principe_de_responsabilit%C3%A9_unique). On évitera donc les `MonEntiteManager` contenant un fourre-tout trop grand de méthodes en rapport plus ou moins lointain avec le concept-métier de l'entité.
- Eviter de référencer un NuGet ASP .NET dans le projet Core (domaine). 

### Paramètres d'entrée et types de retour

- Les services d'**application** ne prendront **en entrée qu'un unique DTO** et produiront **un DTO en sortie**, [comme le préconise la documentation d'ASP .NET Boilerplate](https://aspnetboilerplate.com/Pages/Documents/Application-Services).
- Les services de **domaine** et les **dépôts (repositories) personnalisés** prendront **en entrée**, de préférence, si pertinent, des **interfaces d'entités** (à placer dans le projet `Core.Shared`). Ceci dans le but d'accepeter en entrée aussi bien une entité que l'un de ses DTO's associés. Mais aussi pour éviter la sollicitation trop fréquente des dépôts (repositories). En effet, il est dommage de charger à nouveau une entité pourtant déjà chargée dans l'appelant, simplement parce que le paramètre est un identifiant d'ID. Il est souhaitable que les interfaces en question ne soient pas systématiquement des copies des classes d'entités ou de DTO's mais respectent la [ségrégation d'interfaces](https://fr.wikipedia.org/wiki/Principe_de_s%C3%A9gr%C3%A9gation_des_interfaces) prônée par les principes [SOLID](https://fr.wikipedia.org/wiki/SOLID_(informatique)). Il y a une [discussion à ce sujet sur Stack Overflow](https://stackoverflow.com/questions/5305874/send-domain-entity-as-paremeter-or-send-entity-id-as-parameter-in-application-se).
- Les **dépôts (repositories) personnalisés** produisant une collection retourneront de préférence un `IAsyncQueryable<T>` ou, à défaut, un `IQueryable<T>`. Ceci permettra aux services de domaines de composer les méthodes de tels dépôts entre-eux dans une seule requête LINQ, produisant idéalement une seule requête SQL, du fait de l'usage d'`IQueryable`.

### Performances

- L'**asynchronisme .NET** sera utilisé le plus souvent possible, par l'intermédiaire du couple **`async`**/**`await`** du C# moderne. Ceci dans le but d'améliorer la réactivité du backend.
- **Vérifier par SQL Server Profiler** que les requêtes LINQ soient bien intégralement traduites en SQL, et qu'une partie de la requête n'est pas effectuée par l'application Web plutôt, car Linq-To-Entities n'aurait pas su traduire certaines instructions.
- Mettre à profit AutoMapper dans les requêtes LINQ. AutoMapper peut produire du code traduisible en SQL. Voir [ici](https://codewithstyle.info/solving-entity-framework-performance-issues-automapper/).

# Préférences
Il est préférable de distinguer les deux développements backend :
- **Visual Studio**, pour développer le backend (ASP.NET).

# Hébergement
Les ressources sont regroupées dans le groupe de ressources Heroku-GreenUp et tous les services dédiés sont préfixé par "GreenUp".

## Bases de données
Le serveur SQL utilisé pour les bases de données du projet est : **greenup**

Ce projet comporte 1 base de données PostgreSQL sous Heroku :
- **d3gldok7lrusp3** représente la base de données utilisée pour la prod.

## Serveur
Ce projet comporte 1 App services dédiés au backend (Serveur) :
- **GreenUp-API** représente l'API exposée pour la prod.
- **GreenUp-Front** (https://github.com/marcsim/greenup-front) représente l'application front Angular utilisé pour la prod.



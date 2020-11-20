# MovieStore

MovieStore is online database of information related to films [ASP.Net Core](https://www.asp.net/core/overview/aspnet-vnext)  MVC application

## Key Features

  - Admin interface - REST based system, see your most popular genres, guests locations and manage database(Users,Movies,Genres and Reviews).
  - Recommendation system based on movies user/guest viewed.
  - Integrated with Twitter,TheMovieDB,OMBD,YouTube.
  - Review system: rate and review every movie, every vote counts!

## Tech

MovieStore uses a number of open source projects to work properly:

* [ASP.Net Core](https://www.asp.net/core/overview/aspnet-vnext) - is a free and open-source web framework
* [TheMovieDB](https://www.themoviedb.org/) - our source for images, descreption, genres, rating.
* [OMBD](https://api.igdb.com/) - our source for images, descreption, videos of games.
* [YouTube](https://developers.google.com/apps-script/advanced/youtube) - our source for trailers.
* [3d.js](https://d3js.org/) - great javascript library for graphing stuff
* [W3Layouts](https://w3layouts.com/) - beautiful free template.
* [jQuery](http://jquery.com) - is a JavaScript library designed to simplify the client-side scripting of HTML.

## Installation

MovieStore requires [ASP.Net](https://www.asp.net/core/overview/aspnet-vnext) and [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2017) to run.

 - open ./MovieStore/appsetting.json and make sure to replace all "Your Key" with the relevant API keys.
 - open the sln file on the main folder with Visual Studio, and type the following command in the NuGet console

```sh
$ Update-Database
```
This will update your local SQL Server 30 game loaded from IGDB, give them random prices and create admin user.
```
user: mid@mid.com
password: Qwe123!
```
## Pictures
![HomePage](https://raw.githubusercontent.com/dotanme/GameStoreMid/master/readmepics/main.JPG)

![Movie Page](https://raw.githubusercontent.com/dotanme/GameStoreMid/master/readmepics/products.JPG)

![Graphsh](https://raw.githubusercontent.com/dotanme/GameStoreMid/master/readmepics/similargraph.JPG)

![Dashboard](https://raw.githubusercontent.com/dotanme/GameStoreMid/master/readmepics/prodcutspanel.JPG)

![Movie Review](https://raw.githubusercontent.com/dotanme/GameStoreMid/master/readmepics/prodcutspanel.JPG)




#### This project was made for our Web Application Course.

#### Made By Yariv Menachem(@yariv245) and Afik Menashe(@afikmenashe21).

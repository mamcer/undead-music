# Undead Music

An ASP NET MVC, console and WPF application from 2016

In `original` branch you will find the original source code for this application. In `master` an upgraded, refactored version.

## Description 

UndeadMusic, not a happy name. The reason behind is because this project was started and shortly abandoned several times since 2014 ([soulstone-2](https://github.com/mamcer/soulstone-2)). At the beginning of 2016 I decided to finish the development of this dead-alive project at least at a MVP level. 

The goal of the application was to control playlist and playback of music on remote computers in real time, nothing very different from soulstone-2.

The main difference is a new web responsive front-end. That allows to control the endpoints from a tablet or smartphone.

The main components are a console file scanner application, a SQL Server database, an ASP .NET signalR relay application, a WFP player (reused from soulstone-2) and an ASP .NET web application as front-end. 

## Screenshot

![screenshot](https://raw.githubusercontent.com/mamcer/undead-music/master/doc/screenshot-01.png)

![screenshot](https://raw.githubusercontent.com/mamcer/undead-music/master/doc/screenshot-02.png)

![screenshot](https://raw.githubusercontent.com/mamcer/undead-music/master/doc/screenshot-03.png)

## Technologies

- Visual Studio 2015
- .NET Framework 4.5.2
- Entity Framework 6.1
- SignalR 2.2
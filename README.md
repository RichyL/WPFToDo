# WPFToDo
A small example ToDo list application written in WPF

Functional Design Spec
======================

Plan is to build a base todo list application. It will have	the following functionality:

* [ ] Ability to show a list of tasks from a database
* [ ] Ability to add a task to a database
* [ ] Ability to edit an existing tasks and save the changes to the database
* [ ] Ability to set a task as complete
* [ ] Ability to show all tasks or to filter on completed or opened

The layout of the application is shown in Application_Layout.png

Screens
=======

Add/Edit Tasks -  will use AddEditTaskView
Show existing tasks  - will use ShowAllTasksView

Framework
=========

* Database will be [SQLite](https://www.sqlite.org/index.html)
* MircoORM will be [Dapper](https://github.com/StackExchange/Dapper)
* Testing will be done with [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest). This is the built in testing framework with Visual Studio
* MVVM will be [Stylet](https://github.com/canton7/Stylet)


Database
========

A task can have the following properties:

* Id - NUMBER (Mandary) Primary Key.
* Title - TEXT (Mandatory)
* Description - TEXT (Optional)
* DateTime - TEXT (Mandatory)
* Complete - NUMERIC (Mandatory)

Database to be called TaskDatabase.

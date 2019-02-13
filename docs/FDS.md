Functional Design Spec
======================

Plan is to build a base todo list application. It will have	the following functionality:

* [ ] Ability to show a list of ToDos from a database
* [ ] Ability to add a ToDo to a database
* [ ] Ability to edit an existing ToDos and save the changes to the database
* [ ] Ability to set a ToDo as complete
* [ ] Ability to show all ToDos or to filter on completed or opened

The layout of the application is shown in Application_Layout.png

Screens
=======

Add/Edit ToDos -  will use AddEditToDoView
Show existing ToDos  - will use ShowAllToDosView

Framework
=========

* Database will be [SQLite](https://www.sqlite.org/index.html)
* MircoORM will be [Dapper](https://github.com/StackExchange/Dapper)
* Testing will be done with [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest). This is the built in testing framework with Visual Studio
* MVVM will be [Stylet](https://github.com/canton7/Stylet)


Database
========

A ToDo can have the following properties:

* Id - NUMBER (Mandary) Primary Key.
* Title - TEXT (Mandatory)
* Description - TEXT (Optional)
* DateTime - TEXT (Mandatory)
* Complete - NUMERIC (Mandatory)

Database to be called ToDoDatabase.


How do Screens Work
===================

All tasks are shown by default. There are buttons in the menu to show all, opened and closed tasks. 

If a task is double clicked then it can be edited.

Add tasks will add a new task.
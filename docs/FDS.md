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

The ShowToDo view is shown by default. On this screen the user can:

1. search
2. close a ToDo by clicking on the checkboc
3. open a ToDo by double clicking away from the checkbox
4. filter on all todos, closed todos and open todos (default view)
5. scroll the list of ToDos
6. sort the todos by date ascending and descending
7. Add a new todo = opens EditToDo view


If the user opens a todo then the EditToDo view is shown. This is shown in the same windows as the ShowToDoViewModel

This shows the field for the title and description.
There are save and close buttons.


Methdos on ShowToDoViewModel

1. ToDos properties
2. SetToDoDone(ToDo t)
3. ShowToDo(ToDo t)

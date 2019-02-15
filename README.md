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

Add/Edit ToDos -  will use AddEditToDoView
Show existing tasks  - will use ShowAllToDosView

Framework
=========

* Database will be [SQLite](https://www.sqlite.org/index.html)
* MircoORM will be [Dapper](https://github.com/StackExchange/Dapper)
* Testing will be done with [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest). This is the built in testing framework with Visual Studio
* MVVM will be [Stylet](https://github.com/canton7/Stylet)


Database
========

A todo can have the following properties:

* Id - NUMBER (Mandary) Primary Key.
* Title - TEXT (Mandatory)
* Description - TEXT (Optional)
* DateTime - TEXT (Mandatory)
* Complete - NUMERIC (Mandatory)

Database to be called ToDoDatabase.

Using the repository pattern for the database access.

Interface IToDoDatabase

List<ToDo> GetAllToDos
ToDo GetToDoById
List<ToDo> GetAllOpenToDos
List<ToDo> GetAllClosedToDos
ToDo AddToDo(string title, string description)
bool UpdateToDo
bool DeleteToDo


Description of how the app works

The app starts and it loads currently open ToDos. This list of todos is held in the MainViewModel.

The ShowToDosView will show the current ToDos from this list. If the user double clicks or select an edit button then the screen changes
to the editview. The details of the ToDo are shown ot the user (editable fields like title and description) and not editable fields like creation date.
The editview loads as a new page in the content page of the app. When the user clicks save the ShowToDosView is seen and the changes to the ToDo are seen too. 
Changes are saved to the database at the same time.

If th new ToDo button is pressed then the same view as editToDo is seen. The usre can enter details. Pressing save returns the newly created ToDo which is added to the 
list of ToDos and shown in ShowToDosView.

Objects are passed to the Dapper Repository and the app is not selecting based on ids.

TODO - need to add a search feature.


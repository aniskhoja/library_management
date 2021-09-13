# Library Management System

## File Structure:
- Book_records is the database folder.
- Library Management system, packages, Library management system.sln are project files.

## Installation guide:
This Library Management System is built on the following requirements.

## Requirement:
* Visual Studio 2019
* .Net Framework 4.7.2
* Sql Server 15
* Management studio v18.9.2
 **Note: This requirement is tested on a given version of software and framework. Any other software or version can affect web application functionality.

## Setup
* Download Library Management System web application using Github. 
* Open the Book_records and restore the .bak file in the sql server.
* Now open the “Library Management System.sln” file.
* Inside project open Web.config file and set the database connection string.

## User Guide of LMS(Library Management System) application

### Admin Page
* First click on the Admin button where you can add books and authors. Note: Add author first, before you add a book. You can also add multiple authors. 
After adding new authors, now you can add a book and you choose multiple authors by pressing “ctrl  key”.
* Once you add a book with selected authors you will see book data on the right of your screen. 
* As an admin you can edit, delete and update the quantity of the book.
* Book data also has a user column which shows either a rented (user information) or it will be empty.
* Logout button will take you back to the homepage.

### User Page
* Register as a user by providing your name and email.
* You can use the registered email for login.
* Now you can see book data with the rent option on the right side. Once you click on this it will show another table under the book table, a book which the user has rented.
* Logout button on top will take you back to the homepage.
















